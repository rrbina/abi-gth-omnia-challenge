using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Messages.IntegrationEvents
{
    [ExcludeFromCodeCoverage]
    public class SaleCreatedEvent
    {
        public Guid EventId { get; set; }
        public DateTime EventDateTime { get; set; }
        public Guid? SaleNumber { get; set; }        
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscount { get; set; }

        public SaleCreatedEvent()
        {
                
        }
    }
}