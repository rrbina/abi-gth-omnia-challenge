using System;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Messages.IntegrationEvents
{
    [ExcludeFromCodeCoverage]
    public class CustomerUpdatedEvent
    {
        public Guid EventId { get; set; }
        public DateTime EventDateTime { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime UpdatedAt { get; set; }

        public CustomerUpdatedEvent()
        {
        }
    }
}