using System;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Messages.IntegrationEvents
{
    [ExcludeFromCodeCoverage]
    public class ProductCreatedEvent
    {
        public Guid EventId { get; set; }
        public DateTime EventDateTime { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreatedAt { get; set; }

        public ProductCreatedEvent()
        {
        }
    }
}