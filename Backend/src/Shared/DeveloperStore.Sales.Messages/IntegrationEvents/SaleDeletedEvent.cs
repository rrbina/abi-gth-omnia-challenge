using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Messages.IntegrationEvents
{
    [ExcludeFromCodeCoverage]
    public class SaleDeletedEvent
    {
        public Guid EventId { get; set; }
        public DateTime EventDateTime { get; set; }
        public Guid SaleNumber { get; set; }
        public DateTime DeletedAt { get; set; }

        public SaleDeletedEvent()
        {
                
        }
    }
}