using System;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Messages.IntegrationEvents
{
    [ExcludeFromCodeCoverage]
    public class ProductUpdatedEvent
    {
        public Guid EventId { get; set; }
        public DateTime EventDateTime { get; set; }
        public Guid ProductId { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ProductUpdatedEvent()
        {
        }
    }
}