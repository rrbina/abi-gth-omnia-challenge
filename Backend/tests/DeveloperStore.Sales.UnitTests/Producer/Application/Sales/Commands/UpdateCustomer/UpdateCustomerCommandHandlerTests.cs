using Moq;
using DeveloperStore.Sales.Application.Services.Customers;
using DeveloperStore.Sales.Application.Sales.Commands.UpdateCustomer;
using DeveloperStore.Sales.Application.Common;
using DeveloperStore.Sales.Messages.IntegrationEvents;


namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Call_UpdateCustomerAsync_And_PublishEvent_And_ReturnEventId()
        {
            var customerServiceMock = new Mock<ICustomerService>();
            var eventPublisherMock = new Mock<IEventPublisher>();

            var customerId = Guid.NewGuid();
            var command = new UpdateCustomerCommand(customerId, "Updated Customer");

            var handler = new UpdateCustomerCommandHandler(customerServiceMock.Object, eventPublisherMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, result);

            customerServiceMock.Verify(s => s.UpdateCustomerAsync(command), Times.Once);

            eventPublisherMock.Verify(p => p.PublishAsync(It.Is<CustomerUpdatedEvent>(e =>
                e.CustomerId == customerId &&
                e.EventId == result
            )), Times.Once);
        }
    }
}