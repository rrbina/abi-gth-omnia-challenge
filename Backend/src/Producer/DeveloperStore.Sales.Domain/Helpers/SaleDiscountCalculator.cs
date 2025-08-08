using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Domain.Helpers
{
    public static class SaleDiscountCalculator
    {
        public static decimal CalculateDiscount(int quantity)
        {
            if (quantity > 20)
                throw new InvalidOperationException("Não é permitido vender mais de 20 unidades do mesmo produto.");

            if (quantity >= 10 && quantity <= 20)
                return 0.20m;

            if (quantity >= 4)
                return 0.10m;

            return 0m;
        }
    }
}