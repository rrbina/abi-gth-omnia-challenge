using DeveloperStore.Sales.Application.Sales.Commands.CreateProduct;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Sales.Commands.CreateProduct
{
    public class CreateProductCommandValidatorTests
    {
        [Fact]
        public void Validate_Should_HaveError_When_FieldsAreInvalid()
        {
            var validator = new CreateProductCommandValidator();
            var command = new CreateProductCommand("", 0);
            var result = validator.Validate(command);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_Should_Pass_When_FieldsAreValid()
        {
            var validator = new CreateProductCommandValidator();
            var command = new CreateProductCommand("Produto", 99.9m);
            var result = validator.Validate(command);

            Assert.True(result.IsValid);
        }
    }
}
