using Moq;
using DeveloperStore.Sales.Application.Services.Products;
using DeveloperStore.Sales.Application.Sales.Commands.UpdateProduct;
using DeveloperStore.Sales.Application.Common;
using DeveloperStore.Sales.Messages.IntegrationEvents;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.UpdateProduct
{
    public class UpdateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_Call_UpdateProductAsync_And_PublishEvent_And_ReturnEventId()
        {
            var productServiceMock = new Mock<IProductService>();
            var eventPublisherMock = new Mock<IEventPublisher>();

            var productId = Guid.NewGuid();
            var command = new UpdateProductCommand(productId, "Product Name", 99.99m);

            var handler = new UpdateProductCommandHandler(productServiceMock.Object, eventPublisherMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, result);

            productServiceMock.Verify(s => s.UpdateProductAsync(command), Times.Once);

            eventPublisherMock.Verify(p => p.PublishAsync(It.Is<ProductUpdatedEvent>(e =>
                e.ProductId == productId &&
                e.EventId == result
            )), Times.Once);
        }
    }
}