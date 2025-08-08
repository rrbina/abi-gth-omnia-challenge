using System;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Customer
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }

        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

        public Customer()
        {
            
        }
    }
}