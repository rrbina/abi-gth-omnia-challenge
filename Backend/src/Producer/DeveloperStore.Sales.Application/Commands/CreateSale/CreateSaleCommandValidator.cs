using FluentValidation;

namespace DeveloperStore.Sales.Application.Sales.Commands.CreateSale
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.SaleDto.SaleDate)
                .NotEmpty().WithMessage("Data da venda é obrigatória.");

            RuleFor(x => x.SaleDto.CustomerId)
                .NotEmpty().WithMessage("Cliente é obrigatório.");

            RuleFor(x => x.SaleDto.CustomerName)
                .NotEmpty().WithMessage("Nome do cliente é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.SaleDto.BranchName)
                .NotEmpty().WithMessage("Nome da filial é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.SaleDto.Items)
                .NotEmpty().WithMessage("É necessário pelo menos um item na venda.");

            RuleForEach(x => x.SaleDto.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId)
                    .NotEmpty().WithMessage("Produto é obrigatório.");

                items.RuleFor(i => i.UnitPrice)
                    .GreaterThan(0).WithMessage("Preço unitário deve ser maior que zero.");

                items.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero.")
                    .LessThanOrEqualTo(20).WithMessage("Máximo de 20 unidades por item.");
            });
        }
    }
}