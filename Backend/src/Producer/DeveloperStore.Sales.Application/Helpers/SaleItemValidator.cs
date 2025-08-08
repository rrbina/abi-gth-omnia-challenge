using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Exceptions;
using FluentValidation;

namespace DeveloperStore.Sales.Application.Helpers
{
    public static class SaleItemValidator
    {
        public static void Validate(SaleItem item, decimal discount)
        {
            if (item.Quantity < 4 && discount > 0)
                throw new DeveloperStoreException("Não é permitido aplicar desconto para quantidades inferiores a 4.");            

            var discountValidatorResult = new DiscountValidator().Validate(item);

            if (!discountValidatorResult.IsValid)
                throw new DeveloperStoreException(ValidationMessageHelper.CreateMessageFromFailures(discountValidatorResult.Errors));
        }        
    }
}
