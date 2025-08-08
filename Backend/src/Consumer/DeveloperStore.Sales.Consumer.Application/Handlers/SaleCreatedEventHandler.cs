using DeveloperStore.Sales.Consumer.Application.Helpers;
using DeveloperStore.Sales.Consumer.Application.Services.MongoMessages;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using Rebus.Handlers;
using System.Text.Json;

namespace DeveloperStore.Sales.Consumer.Application.Handler
{
    public class SaleCreatedEventHandler : IHandleMessages<SaleCreatedEvent>
    {
        private readonly IMongoMessageService _mongoMessageService;

        public SaleCreatedEventHandler(IMongoMessageService mongoMessageService)
        {
            _mongoMessageService = mongoMessageService;
        }

        public async Task Handle(SaleCreatedEvent message)
        {
            var helper = new MongoPersistHelper<SaleCreatedEvent>(_mongoMessageService);
            await helper.PersistAsync(message, message.EventId, message.EventDateTime);
        }
    }
}