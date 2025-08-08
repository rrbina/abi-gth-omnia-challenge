using System;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Messages.IntegrationEvents
{
    [ExcludeFromCodeCoverage]
    public class CustomerDeletedEvent
    {
        public Guid EventId { get; set; }
        public DateTime EventDateTime { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime DeletedAt { get; set; }

        public CustomerDeletedEvent()
        {
        }
    }
}