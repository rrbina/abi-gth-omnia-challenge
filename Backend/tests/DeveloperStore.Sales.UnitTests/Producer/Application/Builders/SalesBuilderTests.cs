using DeveloperStore.Sales.Application.Builders;
using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Sales.Commands.CreateSale;
using DeveloperStore.Sales.Domain.Entities;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Builders
{
    public class SalesBuilderTests
    {
        [Fact]
        public void CreateSale_Should_Build_Sale_With_Items_And_Total()
        {
            var command = new CreateSaleCommand
            {
                SaleDto = new SaleDto
                {
                    SaleDate = DateTime.UtcNow,
                    CustomerId = Guid.NewGuid(),
                    BranchName = "Filial",
                    Items = new List<SaleItemDto>
                    {
                        new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 100 },
                        new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 100 },
                        new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 100 },
                        new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 100 }
                    }
                }
            };

            var result = SalesBuilder.CreateSale(command);

            Assert.NotNull(result);
            Assert.Equal(4, result.Items.Count);
            Assert.Equal(command.SaleDto.CustomerId, result.CustomerId);
            Assert.Equal(command.SaleDto.BranchName, result.BranchName);
            Assert.True(result.TotalAmount > 0);
        }

        [Fact]
        public void SetSaleItems_Should_Set_Items_Without_Discount_When_Less_Than_4()
        {
            var sale = new Sale
            {
                SaleNumber = Guid.NewGuid(),
                Items = new List<SaleItem>()
            };

            var items = new List<SaleItemDto>
            {
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 100 },
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 100 },
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 1, UnitPrice = 100 }
            };

            SalesBuilder.SetSaleItems(sale, items);

            Assert.Equal(3, sale.Items.Count);
            Assert.All(sale.Items, i => Assert.Equal(0, i.Discount));
        }

        [Fact]
        public void SetSaleItems_Should_Apply_Discount_When_More_Than_3_Items()
        {
            var sale = new Sale
            {
                SaleNumber = Guid.NewGuid(),
                Items = new List<SaleItem>()
            };

            var items = new List<SaleItemDto>
            {
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 4, UnitPrice = 50 },
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 40 },
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 6, UnitPrice = 30 },
                new SaleItemDto { ProductId = Guid.NewGuid(), Quantity = 7, UnitPrice = 20 }
            };

            SalesBuilder.SetSaleItems(sale, items);

            Assert.Equal(4, sale.Items.Count);
            Assert.All(sale.Items, i => Assert.True(i.Discount > 0));
            Assert.True(sale.TotalDiscount > 0);
        }       
    }
}