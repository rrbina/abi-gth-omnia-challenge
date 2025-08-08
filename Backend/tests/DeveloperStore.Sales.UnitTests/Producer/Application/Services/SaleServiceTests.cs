using Moq;
using DeveloperStore.Sales.Application.Services.Sales;
using DeveloperStore.Sales.Application.Sales.Commands.CreateSale;
using DeveloperStore.Sales.Application.Sales.Commands.UpdateSale;
using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Interfaces.Repositories;
using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Domain.Exceptions;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Services
{
    public class SaleServiceTests
    {
        private readonly Mock<ISaleRepository> _saleRepo = new();
        private readonly Mock<ICustomerRepository> _customerRepo = new();
        private readonly Mock<IProductRepository> _productRepo = new();
        private readonly SaleService _service;

        public SaleServiceTests()
        {
            _service = new SaleService(_saleRepo.Object, _customerRepo.Object, _productRepo.Object);
        }
       

        [Fact]
        public async Task GetSaleByIdAsync_Returns_Null_WhenNotFound()
        {
            _saleRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale)null!);

            var result = await _service.GetSaleByIdAsync(Guid.NewGuid());

            Assert.Null(result);
        }       
        

        [Fact]
        public async Task DeleteSaleAsync_Throws_When_NotFound()
        {
            _saleRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale)null!);

            await Assert.ThrowsAsync<DeveloperStoreException>(() => _service.DeleteSaleAsync(Guid.NewGuid()));
        }
        

        [Fact]
        public async Task CancelSaleAsync_Throws_When_Sale_NotFound()
        {
            _saleRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale)null!);

            await Assert.ThrowsAsync<DeveloperStoreException>(() => _service.CancelSaleAsync(Guid.NewGuid()));
        }

        

        [Fact]
        public async Task CancelItemAsync_Throws_When_Sale_NotFound()
        {
            _saleRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale)null!);

            await Assert.ThrowsAsync<DeveloperStoreException>(() =>
                _service.CancelItemAsync(Guid.NewGuid(), Guid.NewGuid()));
        }

        [Fact]
        public async Task CancelItemAsync_Throws_When_Item_NotFound()
        {
            var sale = new Sale { SaleNumber = Guid.NewGuid(), Items = new List<SaleItem>() };

            _saleRepo.Setup(r => r.GetByIdAsync(sale.SaleNumber)).ReturnsAsync(sale);

            await Assert.ThrowsAsync<DeveloperStoreException>(() =>
                _service.CancelItemAsync(sale.SaleNumber, Guid.NewGuid()));
        }    

        [Fact]
        public async Task UpdateSaleAsync_Throws_When_Sale_NotFound()
        {
            _saleRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Sale)null!);

            var command = new UpdateSaleCommand
            {
                SaleNumber = Guid.NewGuid(),
                Items = new List<SaleItemDto>()
            };

            await Assert.ThrowsAsync<DeveloperStoreException>(() => _service.UpdateSaleAsync(command));
        }

        [Fact]
        public async Task CreateSaleAsync_Creates_And_Returns_Sale()
        {
            var command = new CreateSaleCommand
            {
                SaleDto = new SaleDto
                {
                    CustomerId = Guid.NewGuid(),
                    CustomerName = "Cliente",
                    BranchName = "Filial",
                    SaleDate = DateTime.UtcNow,
                    Items = new List<SaleItemDto>
                    {
                        new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 10 }
                    }
                }
            };

            var product = new Product { Id = command.SaleDto.Items.First().ProductId, UnitPrice = 10 };

            _customerRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Customer());
            _productRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);

            var result = await _service.CreateSaleAsync(command);

            Assert.NotNull(result);
            Assert.Equal(command.SaleDto.CustomerId, result.CustomerId);
        }
    }
}