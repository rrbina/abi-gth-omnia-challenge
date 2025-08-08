using FluentValidation;

namespace DeveloperStore.Sales.Application.Sales.Commands.UpdateSale
{
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty().WithMessage("Número da venda é obrigatório.");

            RuleFor(x => x.SaleDate)
                .NotEmpty().WithMessage("Data da venda é obrigatória.");

            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Cliente é obrigatório.");

            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Nome do cliente é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.BranchId)
                .NotEmpty().WithMessage("Filial é obrigatória.");

            RuleFor(x => x.BranchName)
                .NotEmpty().WithMessage("Nome da filial é obrigatório.")
                .MaximumLength(100);

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("É necessário pelo menos um item na venda.");

            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.Id)
                    .NotEmpty().WithMessage("Id do item é obrigatório.");

                items.RuleFor(i => i.ProductId)
                    .NotEmpty().WithMessage("Produto é obrigatório.");                

                items.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Quantidade deve ser maior que zero.")
                    .LessThanOrEqualTo(20).WithMessage("Máximo de 20 unidades por item.");

                items.RuleFor(i => i.UnitPrice)
                    .GreaterThan(0).WithMessage("Preço unitário deve ser maior que zero.");
            });
        }
    }
}