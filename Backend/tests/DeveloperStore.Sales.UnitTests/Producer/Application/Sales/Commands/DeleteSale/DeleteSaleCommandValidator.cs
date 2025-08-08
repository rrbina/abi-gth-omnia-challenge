using DeveloperStore.Sales.Application.Sales.Commands.DeleteSale;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.DeleteSale
{
    public class DeleteSaleCommandValidatorTests
    {
        private readonly DeleteSaleCommandValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new DeleteSaleCommand(Guid.Empty);
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Pass_When_Id_Is_Valid()
        {
            var command = new DeleteSaleCommand(Guid.NewGuid());
            var result = _validator.Validate(command);
            Assert.True(result.IsValid);
        }
    }
}