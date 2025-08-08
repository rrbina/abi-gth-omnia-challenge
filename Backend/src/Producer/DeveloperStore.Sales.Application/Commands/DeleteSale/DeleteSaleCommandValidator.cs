using DeveloperStore.Sales.Application.Sales.Commands.DeleteProduct;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.DeleteSale
{
    [ExcludeFromCodeCoverage]
    public class DeleteSaleCommandValidator : AbstractValidator<DeleteSaleCommand>
    {
        public DeleteSaleCommandValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty().WithMessage("O Sale Number é obrigatório.");
        }
    }
}