using DeveloperStore.Sales.Application.Sales.Commands.DeleteCustomer;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidatorTests
    {
        private readonly DeleteCustomerCommandValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new DeleteCustomerCommand(Guid.Empty);
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Pass_When_Id_Is_Valid()
        {
            var command = new DeleteCustomerCommand(Guid.NewGuid());
            var result = _validator.Validate(command);
            Assert.True(result.IsValid);
        }
    }
}
