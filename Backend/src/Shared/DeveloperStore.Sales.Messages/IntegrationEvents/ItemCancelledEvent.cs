using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Messages.IntegrationEvents
{
    [ExcludeFromCodeCoverage]
    public class ItemCancelledEvent
    {
        public Guid EventId { get; set; }
        public DateTime EventDateTime { get; set; }
        public Guid SaleNumber { get; set; }
        public Guid ItemId { get; set; }
        public DateTime CancelledAt { get; set; }

        public ItemCancelledEvent()
        {
                
        }
    }
}