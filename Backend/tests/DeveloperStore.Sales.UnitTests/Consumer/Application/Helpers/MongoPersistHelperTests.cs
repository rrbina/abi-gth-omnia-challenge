using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using DeveloperStore.Sales.Consumer.Application.Helpers;
using DeveloperStore.Sales.Consumer.Application.Services.MongoMessages;

namespace DeveloperStore.Sales.UnitTests.Consumer.Application.Helpers
{
    public class MongoPersistHelperTests
    {
        private class DummyEvent
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        [Fact]
        public async Task PersistAsync_Should_Call_PersistMessageAsync_When_Service_Is_Not_Null()
        {
            var message = new DummyEvent { Id = Guid.NewGuid(), Name = "Test" };
            var eventId = Guid.NewGuid();
            var dateTime = DateTime.UtcNow;

            var mongoServiceMock = new Mock<IMongoMessageService>();

            var helper = new MongoPersistHelper<DummyEvent>(mongoServiceMock.Object);

            await helper.PersistAsync(message, eventId, dateTime);

            mongoServiceMock.Verify(s =>
                s.PersistMessageAsync(
                    It.Is<string>(json => json.Contains("Test")),
                    eventId,
                    dateTime
                ), Times.Once);
        }

        [Fact]
        public async Task PersistAsync_Should_Do_Nothing_When_Service_Is_Null()
        {
            var message = new DummyEvent { Id = Guid.NewGuid(), Name = "ShouldNotPersist" };
            var helper = new MongoPersistHelper<DummyEvent>(null);

            // Não deve lançar exceção nem persistir
            await helper.PersistAsync(message, Guid.NewGuid(), DateTime.UtcNow);
        }
    }
}