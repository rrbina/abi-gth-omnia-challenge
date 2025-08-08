using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Product
    {
        public Guid Id { get; set; }        
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
