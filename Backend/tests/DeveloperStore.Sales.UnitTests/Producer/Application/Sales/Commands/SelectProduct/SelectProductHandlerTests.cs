using Moq;
using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Sales.Handlers;
using DeveloperStore.Sales.Application.Sales.Commands.SelectProduct;
using DeveloperStore.Sales.Application.Services.Products;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.SelectProduct
{
    public class SelectProductHandlerTests
    {
        [Fact]
        public async Task Handle_GetAllProducts_Should_ReturnProductList()
        {
            var expectedProducts = new List<ProductDto>
            {
                new ProductDto { Id = Guid.NewGuid(), ProductName = "Produto A", UnitPrice = 12.5m },
                new ProductDto { Id = Guid.NewGuid(), ProductName = "Produto B", UnitPrice = 25.0m }
            };

            var productServiceMock = new Mock<IProductService>();
            productServiceMock
                .Setup(s => s.GetAllProductsAsync())
                .ReturnsAsync(expectedProducts);

            var handler = new SelectProductHandler(productServiceMock.Object);
            var result = await handler.Handle(new GetAllProductsCommand(), CancellationToken.None);

            Assert.Equal(expectedProducts, result);
        }

        [Fact]
        public async Task Handle_GetProductById_Should_ReturnProduct()
        {
            var productId = Guid.NewGuid();
            var expectedProduct = new ProductDto { Id = productId, ProductName = "Produto C", UnitPrice = 50.0m };

            var productServiceMock = new Mock<IProductService>();
            productServiceMock
                .Setup(s => s.GetProductByIdAsync(productId))
                .ReturnsAsync(expectedProduct);

            var handler = new SelectProductHandler(productServiceMock.Object);
            var result = await handler.Handle(new GetProductByIdCommand(productId), CancellationToken.None);

            Assert.Equal(expectedProduct, result);
        }
    }
}
