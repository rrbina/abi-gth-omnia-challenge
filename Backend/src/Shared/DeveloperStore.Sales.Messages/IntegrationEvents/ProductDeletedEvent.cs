using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Messages.IntegrationEvents
{
    [ExcludeFromCodeCoverage]
    public class ProductDeletedEvent
    {
        public Guid EventId { get; set; }
        public DateTime EventDateTime { get; set; }
        public Guid ProductId { get; set; }
        public DateTime DeletedAt { get; set; }

        public ProductDeletedEvent()
        {
        }
    }
}