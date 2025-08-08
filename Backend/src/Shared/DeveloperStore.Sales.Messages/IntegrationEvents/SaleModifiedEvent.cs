using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Messages.IntegrationEvents
{
    [ExcludeFromCodeCoverage]
    public class SaleModifiedEvent
    {
        public Guid EventId { get; set; }
        public DateTime EventDateTime { get; set; }
        public Guid SaleNumber { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal NewTotalAmount { get; set; }

        public SaleModifiedEvent()
        {
                
        }
    }
}