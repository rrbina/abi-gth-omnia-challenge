using DeveloperStore.Sales.Consumer.Application.Helpers;
using DeveloperStore.Sales.Consumer.Application.Services.MongoMessages;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using Rebus.Handlers;
using System.Text.Json;

namespace DeveloperStore.Sales.Consumer.Application.Handler
{
    public class SaleModifiedEventHandler : IHandleMessages<SaleModifiedEvent>
    {      
        private readonly IMongoMessageService _mongoMessageService;

        public SaleModifiedEventHandler(IMongoMessageService mongoMessageService)
        {
            _mongoMessageService = mongoMessageService;
        }

        public async Task Handle(SaleModifiedEvent message)
        {
            var json = JsonSerializer.Serialize(message);
            var helper = new MongoPersistHelper<SaleModifiedEvent>(_mongoMessageService);
            await helper.PersistAsync(message, message.EventId, message.EventDateTime);
        }
    }
}