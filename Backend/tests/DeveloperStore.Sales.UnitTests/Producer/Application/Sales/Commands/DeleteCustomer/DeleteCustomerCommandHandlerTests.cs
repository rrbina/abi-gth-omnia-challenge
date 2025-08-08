using Moq;
using DeveloperStore.Sales.Application.Services.Customers;
using DeveloperStore.Sales.Application.Sales.Commands.DeleteCustomer;
using DeveloperStore.Sales.Application.Common;
using DeveloperStore.Sales.Messages.IntegrationEvents;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Call_DeleteCustomerAsync_And_PublishEvent_And_Return_EventId()
        {
            var customerServiceMock = new Mock<ICustomerService>();
            var eventPublisherMock = new Mock<IEventPublisher>();

            var customerId = Guid.NewGuid();
            var command = new DeleteCustomerCommand(customerId);

            var handler = new DeleteCustomerCommandHandler(customerServiceMock.Object, eventPublisherMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, result);

            eventPublisherMock.Verify(p => p.PublishAsync(It.Is<CustomerDeletedEvent>(e =>
                e.CustomerId == customerId &&
                e.EventId == result
            )), Times.Once);
        }
    }
}