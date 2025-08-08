using DeveloperStore.Sales.Application.Sales.Commands.CancelSale;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.CancelSale
{
    public class CancelSaleCommandValidatorTests
    {
        [Fact]
        public void Validate_Should_HaveError_When_SaleNumberIsEmpty()
        {
            var validator = new CancelSaleCommandValidator();
            var command = new CancelSaleCommand(Guid.Empty);
            var result = validator.Validate(command);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_Should_Pass_When_SaleNumberIsValid()
        {
            var validator = new CancelSaleCommandValidator();
            var command = new CancelSaleCommand(Guid.NewGuid());
            var result = validator.Validate(command);

            Assert.True(result.IsValid);
        }
    }
}
