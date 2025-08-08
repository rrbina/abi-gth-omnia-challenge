using Moq;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using DeveloperStore.Sales.Application.Behaviors;
using DeveloperStore.Sales.Domain.Exceptions;

namespace DeveloperStore.Sales.UnitTests.Producer.Application.Behaviors
{
    public class ValidationBehaviorTests
    {
        [Fact]
        public async Task Handle_Should_ThrowValidationException_When_ValidationFails()
        {
            var validatorMock = new Mock<IValidator<TestRequest>>();
            var validationFailure = new ValidationFailure("Property", "Error message");

            validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult(new[] { validationFailure }));

            var behavior = new ValidationBehavior<TestRequest, string>(new List<IValidator<TestRequest>> { validatorMock.Object });

            var request = new TestRequest();

            var next = new Mock<RequestHandlerDelegate<string>>();

            await Assert.ThrowsAsync<DeveloperStoreException>(() =>
                behavior.Handle(request, next.Object, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_Should_CallNext_When_ValidationPasses()
        {
            var validatorMock = new Mock<IValidator<TestRequest>>();

            validatorMock
                .Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            var behavior = new ValidationBehavior<TestRequest, string>(new List<IValidator<TestRequest>> { validatorMock.Object });

            var request = new TestRequest();

            var next = new Mock<RequestHandlerDelegate<string>>();
            next.Setup(n => n()).ReturnsAsync("success");

            var result = await behavior.Handle(request, next.Object, CancellationToken.None);

            Assert.Equal("success", result);
            next.Verify(n => n(), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_SkipValidation_When_NoValidators()
        {
            var behavior = new ValidationBehavior<TestRequest, string>(new List<IValidator<TestRequest>>());

            var request = new TestRequest();

            var next = new Mock<RequestHandlerDelegate<string>>();
            next.Setup(n => n()).ReturnsAsync("no-validation");

            var result = await behavior.Handle(request, next.Object, CancellationToken.None);

            Assert.Equal("no-validation", result);
            next.Verify(n => n(), Times.Once);
        }
    }

    public class TestRequest : IRequest<string> { }
}
