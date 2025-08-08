using DeveloperStore.Sales.Application.Sales.Commands.UpdateProduct;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.UpdateProduct
{
    public class UpdateProductCommandValidatorTests
    {
        private readonly UpdateProductCommandValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new UpdateProductCommand(Guid.Empty, "Produto", 10);
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var command = new UpdateProductCommand(Guid.NewGuid(), "", 10);
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_Name_Too_Long()
        {
            var command = new UpdateProductCommand(Guid.NewGuid(), new string('A', 101), 10);
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Have_Error_When_UnitPrice_Is_Zero()
        {
            var command = new UpdateProductCommand(Guid.NewGuid(), "Produto", 0);
            var result = _validator.Validate(command);
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Should_Pass_When_Command_Is_Valid()
        {
            var command = new UpdateProductCommand(Guid.NewGuid(), "Produto Válido", 25);
            var result = _validator.Validate(command);
            Assert.True(result.IsValid);
        }
    }
}
