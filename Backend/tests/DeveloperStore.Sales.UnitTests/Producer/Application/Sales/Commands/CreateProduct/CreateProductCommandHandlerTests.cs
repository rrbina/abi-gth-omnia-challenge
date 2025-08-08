using Moq;
using DeveloperStore.Sales.Application.Sales.Commands.CreateProduct;
using DeveloperStore.Sales.Application.Services.Products;
using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Common;
using DeveloperStore.Sales.Messages.IntegrationEvents;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.CreateProduct
{
    public class CreateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_Should_PublishEvent_AndReturnEventId_When_ProductIsCreated()
        {
            var command = new CreateProductCommand { ProductName = "Produto Teste", UnitPrice = 10.5m };
            var productId = Guid.NewGuid();

            var productServiceMock = new Mock<IProductService>();
            productServiceMock
                .Setup(s => s.CreateProductAsync(It.IsAny<CreateProductCommand>()))
                .ReturnsAsync(new ProductDto { Id = productId });

            var eventPublisherMock = new Mock<IEventPublisher>();

            var handler = new CreateProductCommandHandler(productServiceMock.Object, eventPublisherMock.Object);
            var result = await handler.Handle(command, CancellationToken.None);

            Assert.NotEqual(Guid.Empty, result);

            eventPublisherMock.Verify(p => p.PublishAsync(It.Is<ProductCreatedEvent>(e =>
                e.ProductId == productId &&
                e.EventId == result
            )), Times.Once);
        }
    }
}