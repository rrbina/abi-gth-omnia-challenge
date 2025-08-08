using FluentValidation;

namespace DeveloperStore.Sales.Application.Sales.Commands.CancelSale
{
    public class CancelSaleCommandValidator : AbstractValidator<CancelSaleCommand>
    {
        public CancelSaleCommandValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty().WithMessage("Id da venda é obrigatório.");
        }
    }
}