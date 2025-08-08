using DeveloperStore.Sales.Application.Sales.Commands.CreateSale;
using DeveloperStore.Sales.Application.DTOs;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.CreateSale
{
    public class CreateSaleCommandValidatorTests
    {
        private readonly CreateSaleCommandValidator _validator = new();
        

        [Fact]
        public void Should_Have_Error_When_CustomerId_Is_Empty()
        {
            var command = new CreateSaleCommand
            {
                SaleDto = new SaleDto
                {
                    CustomerId = Guid.Empty,
                    CustomerName = "Cliente",
                    BranchName = "Filial",
                    Items = new List<SaleItemDto>()
                }
            };

            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_CustomerName_Is_Empty()
        {
            var command = new CreateSaleCommand
            {
                SaleDto = new SaleDto
                {
                    CustomerId = Guid.NewGuid(),
                    CustomerName = "",
                    BranchName = "Filial",
                    Items = new List<SaleItemDto>()
                }
            };

            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_BranchName_Is_Empty()
        {
            var command = new CreateSaleCommand
            {
                SaleDto = new SaleDto
                {
                    CustomerId = Guid.NewGuid(),
                    CustomerName = "Cliente",
                    BranchName = "",
                    Items = new List<SaleItemDto>()
                }
            };

            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Items_Is_Null()
        {
            var command = new CreateSaleCommand
            {
                SaleDto = new SaleDto
                {
                    CustomerId = Guid.NewGuid(),
                    CustomerName = "Cliente",
                    BranchName = "Filial",
                    Items = null
                }
            };

            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Pass_When_SaleDto_Is_Valid()
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
                        new SaleItemDto
                        {
                            Id = Guid.NewGuid(),
                            ProductId = Guid.NewGuid(),
                            Quantity = 1,
                            UnitPrice = 50,
                            Discount = 0
                        }
                    }
                }
            };

            var result = _validator.Validate(command);
            Assert.True(result.IsValid);
        }
    }
}