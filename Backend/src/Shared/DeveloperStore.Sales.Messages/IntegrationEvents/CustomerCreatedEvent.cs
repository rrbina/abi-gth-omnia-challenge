using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Messages.IntegrationEvents
{
    [ExcludeFromCodeCoverage]
    public class CustomerCreatedEvent
    {
        public Guid EventId { get; set; }
        public DateTime EventDateTime { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }

        public CustomerCreatedEvent()
        {
        }
    }
}