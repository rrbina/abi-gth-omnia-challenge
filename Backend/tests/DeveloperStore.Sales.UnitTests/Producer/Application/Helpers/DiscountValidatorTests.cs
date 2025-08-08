using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Application.Helpers;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Helpers
{
    public class DiscountValidatorTests
    {
        private readonly DiscountValidator _validator = new();

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(3, 0)]
        public void Discount_Should_Be_Zero_For_Less_Than_4(int quantity, decimal discount)
        {
            var item = new SaleItem
            {
                Quantity = quantity,
                Discount = discount
            };

            var result = _validator.Validate(item);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(4, 0.1)]
        [InlineData(5, 0.1)]
        [InlineData(9, 0.1)]
        public void Discount_Should_Be_10_Percent_For_Quantity_Between_4_And_9(int quantity, decimal discount)
        {
            var item = new SaleItem
            {
                Quantity = quantity,
                Discount = discount
            };

            var result = _validator.Validate(item);

            Assert.True(result.IsValid);
        }

        [Theory]
        [InlineData(10, 0.2)]
        [InlineData(15, 0.2)]
        [InlineData(20, 0.2)]
        public void Discount_Should_Be_20_Percent_For_Quantity_Between_10_And_20(int quantity, decimal discount)
        {
            var item = new SaleItem
            {
                Quantity = quantity,
                Discount = discount
            };

            var result = _validator.Validate(item);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void Discount_Invalid_For_Quantity_3_Discount_0_1()
        {
            var item = new SaleItem { Quantity = 3, Discount = 0.1m };
            var result = _validator.Validate(item);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Discount_Invalid_For_Quantity_4_Discount_0_2()
        {
            var item = new SaleItem { Quantity = 4, Discount = 0.2m };
            var result = _validator.Validate(item);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Discount_Invalid_For_Quantity_10_Discount_0_1()
        {
            var item = new SaleItem { Quantity = 10, Discount = 0.1m };
            var result = _validator.Validate(item);
            Assert.False(result.IsValid);
        }
    }
}
