using DeveloperStore.Sales.Application.Sales.Commands.UpdateSale;
using DeveloperStore.Sales.Application.DTOs;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.UpdateSale
{
    public class UpdateSaleCommandValidatorTests
    {
        private readonly UpdateSaleCommandValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_SaleNumber_Is_Empty()
        {
            var command = new UpdateSaleCommand
            {
                SaleNumber = Guid.Empty,
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "Cliente",
                BranchId = Guid.NewGuid(),
                BranchName = "Filial",
                Items = new List<SaleItemDto>()
            };

            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_CustomerId_Is_Empty()
        {
            var command = new UpdateSaleCommand
            {
                SaleNumber = Guid.NewGuid(),
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.Empty,
                CustomerName = "Cliente",
                BranchId = Guid.NewGuid(),
                BranchName = "Filial",
                Items = new List<SaleItemDto>()
            };

            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_CustomerName_Is_Empty()
        {
            var command = new UpdateSaleCommand
            {
                SaleNumber = Guid.NewGuid(),
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "",
                BranchId = Guid.NewGuid(),
                BranchName = "Filial",
                Items = new List<SaleItemDto>()
            };

            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_BranchId_Is_Empty()
        {
            var command = new UpdateSaleCommand
            {
                SaleNumber = Guid.NewGuid(),
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "Cliente",
                BranchId = Guid.Empty,
                BranchName = "Filial",
                Items = new List<SaleItemDto>()
            };

            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_BranchName_Is_Empty()
        {
            var command = new UpdateSaleCommand
            {
                SaleNumber = Guid.NewGuid(),
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "Cliente",
                BranchId = Guid.NewGuid(),
                BranchName = "",
                Items = new List<SaleItemDto>()
            };

            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Items_Is_Null()
        {
            var command = new UpdateSaleCommand
            {
                SaleNumber = Guid.NewGuid(),
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "Cliente",
                BranchId = Guid.NewGuid(),
                BranchName = "Filial",
                Items = null
            };

            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Pass_When_Command_Is_Valid()
        {
            var command = new UpdateSaleCommand
            {
                SaleNumber = Guid.NewGuid(),
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "Cliente",
                BranchId = Guid.NewGuid(),
                BranchName = "Filial",
                Items = new List<SaleItemDto>
                {
                    new SaleItemDto
                    {
                        Id = Guid.NewGuid(),
                        ProductId = Guid.NewGuid(),
                        Quantity = 1,
                        UnitPrice = 10,
                        Discount = 0
                    }
                }
            };

            var result = _validator.Validate(command);
            Assert.True(result.IsValid);
        }
    }
}
