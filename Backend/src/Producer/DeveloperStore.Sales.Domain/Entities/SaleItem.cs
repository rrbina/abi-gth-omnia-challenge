
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class SaleItem
    {
        public Guid Id { get; set; }      
        public int Quantity { get; set; }        
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsCancelled { get; set; }

        public Guid SaleId { get; set; }
        public virtual Sale Sale { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}