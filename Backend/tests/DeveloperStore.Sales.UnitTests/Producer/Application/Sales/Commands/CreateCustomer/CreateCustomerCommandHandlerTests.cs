using Moq;
using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Sales.Commands.CreateCustomer;
using DeveloperStore.Sales.Application.Services.Customers;
using DeveloperStore.Sales.Application.Common;
using DeveloperStore.Sales.Messages.IntegrationEvents;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_PublishEvent_AndReturnEventId_When_CustomerIsCreated()
        {
            var customerId = Guid.NewGuid();
            var command = new CreateCustomerCommand("Cliente");

            var customerServiceMock = new Mock<ICustomerService>();
            customerServiceMock
                .Setup(s => s.CreateCustomerAsync(command))
                .ReturnsAsync(new CustomerDto { Id = customerId });

            var eventPublisherMock = new Mock<IEventPublisher>();

            var handler = new CreateCustomerCommandHandler(customerServiceMock.Object, eventPublisherMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, result);

            eventPublisherMock.Verify(p => p.PublishAsync(It.Is<CustomerCreatedEvent>(e =>
                e.CustomerId == customerId &&
                e.EventId == result
            )), Times.Once);
        }
    }
}