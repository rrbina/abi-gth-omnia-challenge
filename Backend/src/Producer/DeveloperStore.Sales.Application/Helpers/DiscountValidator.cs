using DeveloperStore.Sales.Domain.Entities;
using FluentValidation;

namespace DeveloperStore.Sales.Application.Helpers
{
    public class DiscountValidator : AbstractValidator<SaleItem>
    {
        public DiscountValidator()
        {
            RuleFor(i => i.Discount)
            .Equal(0).WithMessage("Descontos não são permitidos para menos de 4 unidades.")
            .When(i => i.Quantity < 4);            

            RuleFor(i => i.Discount)
            .Equal(0.1m)
            .When(i => i.Quantity >= 4 && i.Quantity < 10)
            .WithMessage("Desconto deve ser de 10% para mais de 4 unidades do mesmo item.");

            RuleFor(i => i.Discount)
            .Equal(0.2m)
            .When(i => i.Quantity >= 10 && i.Quantity <= 20)            
            .WithMessage("Desconto deve ser de 20% para quantidades entre 10 e 20.");
        }
    }
}
