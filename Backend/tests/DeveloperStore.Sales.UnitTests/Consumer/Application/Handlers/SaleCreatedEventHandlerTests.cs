using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using DeveloperStore.Sales.Consumer.Application.Handler;
using DeveloperStore.Sales.Consumer.Application.Services.MongoMessages;
using DeveloperStore.Sales.Messages.IntegrationEvents;

namespace DeveloperStore.Sales.UnitTests.Consumer.Application.Handlers
{
    public class SaleCreatedEventHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Persist_Event_Correctly()
        {
            var mongoServiceMock = new Mock<IMongoMessageService>();
            var handler = new SaleCreatedEventHandler(mongoServiceMock.Object);

            var testEvent = new SaleCreatedEvent
            {
                EventId = Guid.NewGuid(),
                EventDateTime = DateTime.UtcNow,
                CustomerName = "Teste",
                SaleNumber = Guid.NewGuid(),
                TotalAmount = 10m,
                TotalDiscount = 0.5m
            };

            await handler.Handle(testEvent);

            mongoServiceMock.Verify(s =>
                s.PersistMessageAsync(
                    It.Is<string>(msg =>
                        msg.Contains(testEvent.EventId.ToString()) &&
                        msg.Contains(testEvent.SaleNumber.ToString()) &&
                        msg.Contains("Teste")
                    ),
                    testEvent.EventId,
                    testEvent.EventDateTime),
                Times.Once);
        }
    }
}
