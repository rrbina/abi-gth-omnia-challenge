using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using DeveloperStore.Sales.Consumer.Application.Handler;
using DeveloperStore.Sales.Consumer.Application.Services.MongoMessages;
using DeveloperStore.Sales.Messages.IntegrationEvents;

namespace DeveloperStore.Sales.UnitTests.Consumer.Application.Handlers
{
    public class ItemCancelledEventHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Persist_Event_Correctly()
        {
            var mongoMessageServiceMock = new Mock<IMongoMessageService>();
            var handler = new ItemCancelledEventHandler(mongoMessageServiceMock.Object);

            var testEvent = new ItemCancelledEvent
            {
                EventId = Guid.NewGuid(),
                EventDateTime = DateTime.UtcNow,
                SaleNumber = Guid.NewGuid(),
                ItemId = Guid.NewGuid(),
                CancelledAt = DateTime.UtcNow
            };

            await handler.Handle(testEvent);

            mongoMessageServiceMock.Verify(s =>
                s.PersistMessageAsync(
                    It.Is<string>(msg =>
                        msg.Contains(testEvent.EventId.ToString()) &&
                        msg.Contains(testEvent.SaleNumber.ToString()) &&
                        msg.Contains(testEvent.ItemId.ToString())
                    ),
                    testEvent.EventId,
                    testEvent.EventDateTime),
                Times.Once);
        }
    }
}
