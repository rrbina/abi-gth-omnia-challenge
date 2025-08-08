using Moq;
using DeveloperStore.Sales.Application.Services.Products;
using DeveloperStore.Sales.Application.Sales.Commands.DeleteProduct;
using DeveloperStore.Sales.Application.Common;
using DeveloperStore.Sales.Messages.IntegrationEvents;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.DeleteProduct
{
    public class DeleteProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Call_DeleteProductAsync_And_PublishEvent_And_Return_EventId()
        {
            var productServiceMock = new Mock<IProductService>();
            var eventPublisherMock = new Mock<IEventPublisher>();

            var productId = Guid.NewGuid();
            var command = new DeleteProductCommand(productId);

            var handler = new DeleteProductCommandHandler(productServiceMock.Object, eventPublisherMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, result);

            eventPublisherMock.Verify(p => p.PublishAsync(It.Is<ProductDeletedEvent>(e =>
                e.ProductId == productId &&
                e.EventId == result
            )), Times.Once);
        }
    }
}