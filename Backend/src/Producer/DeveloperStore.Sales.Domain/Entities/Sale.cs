using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Sale
    {
        public Guid SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }        
        public string BranchName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }
        public bool IsCancelled { get; set; }       

        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
    }
}