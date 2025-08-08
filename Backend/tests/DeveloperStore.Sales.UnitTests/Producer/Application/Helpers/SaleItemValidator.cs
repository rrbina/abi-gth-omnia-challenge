using DeveloperStore.Sales.Application.Helpers;
using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Exceptions;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Helpers
{
    public class SaleItemValidatorTests
    {
        [Fact]
        public void Should_Throw_When_Discount_Is_Given_For_Quantity_Less_Than_4()
        {
            var item = new SaleItem
            {
                Quantity = 2,
                Discount = 0.1m
            };

            var ex = Assert.Throws<DeveloperStoreException>(() =>
                SaleItemValidator.Validate(item, item.Discount));

            Assert.Equal("Não é permitido aplicar desconto para quantidades inferiores a 4.", ex.Message);
        }

        [Fact]
        public void Should_Throw_When_Discount_Is_Invalid_Per_DiscountValidator()
        {
            var item = new SaleItem
            {
                Quantity = 5,
                Discount = 0.2m // deveria ser 0.1
            };

            var ex = Assert.Throws<DeveloperStoreException>(() =>
                SaleItemValidator.Validate(item, item.Discount));

            Assert.Contains("Desconto deve ser de 10%", ex.Message);
        }

        [Fact]
        public void Should_Not_Throw_When_Discount_Is_Valid_For_Quantity_Less_Than_4()
        {
            var item = new SaleItem
            {
                Quantity = 2,
                Discount = 0
            };

            Exception? ex = Record.Exception(() =>
                SaleItemValidator.Validate(item, item.Discount));

            Assert.Null(ex);
        }

        [Fact]
        public void Should_Not_Throw_When_Discount_Is_Valid_For_Quantity_4_9()
        {
            var item = new SaleItem
            {
                Quantity = 5,
                Discount = 0.1m
            };

            Exception? ex = Record.Exception(() =>
                SaleItemValidator.Validate(item, item.Discount));

            Assert.Null(ex);
        }

        [Fact]
        public void Should_Not_Throw_When_Discount_Is_Valid_For_Quantity_10_20()
        {
            var item = new SaleItem
            {
                Quantity = 15,
                Discount = 0.2m
            };

            Exception? ex = Record.Exception(() =>
                SaleItemValidator.Validate(item, item.Discount));

            Assert.Null(ex);
        }
    }
}