using DeveloperStore.Sales.Consumer.Application.Helpers;
using DeveloperStore.Sales.Consumer.Application.Services.MongoMessages;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using Rebus.Handlers;
using System.Text.Json;

namespace DeveloperStore.Sales.Consumer.Application.Handler
{
    public class ProductUpdatedEventHandler : IHandleMessages<ProductUpdatedEvent>
    {
        private readonly IMongoMessageService _mongoMessageService;

        public ProductUpdatedEventHandler(IMongoMessageService mongoMessageService)
        {
            _mongoMessageService = mongoMessageService;
        }

        public async Task Handle(ProductUpdatedEvent message)
        {
            var json = JsonSerializer.Serialize(message);
            var helper = new MongoPersistHelper<ProductUpdatedEvent>(_mongoMessageService);
            await helper.PersistAsync(message, message.EventId, message.EventDateTime);
        }
    }
}