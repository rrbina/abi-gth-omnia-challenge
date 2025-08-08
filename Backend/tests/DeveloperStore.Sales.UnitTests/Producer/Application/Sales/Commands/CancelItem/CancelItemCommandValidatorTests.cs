using DeveloperStore.Sales.Application.Sales.Commands.CancelItem;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.CancelItem
{
    public class CancelItemCommandValidatorTests
    {
        [Fact]
        public void Validate_Should_HaveError_When_SaleNumberOrItemIdIsEmpty()
        {
            var validator = new CancelItemCommandValidator();

            var command = new CancelItemCommand(Guid.Empty, Guid.Empty);

            var result = validator.Validate(command);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_Should_Pass_When_IdsAreValid()
        {
            var validator = new CancelItemCommandValidator();

            var command = new CancelItemCommand(Guid.NewGuid(), Guid.NewGuid());

            var result = validator.Validate(command);

            Assert.True(result.IsValid);
        }
    }
}
