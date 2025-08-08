using DeveloperStore.Sales.Consumer.Application.Helpers;
using DeveloperStore.Sales.Consumer.Application.Services.MongoMessages;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using Rebus.Handlers;
using System.Text.Json;

namespace DeveloperStore.Sales.Consumer.Application.Handlers
{
    public class SaleDeletedEventHandler : IHandleMessages<SaleDeletedEvent>
    {
        private readonly IMongoMessageService _mongoMessageService;

        public SaleDeletedEventHandler(IMongoMessageService mongoMessageService)
        {
            _mongoMessageService = mongoMessageService;
        }

        public async Task Handle(SaleDeletedEvent message)
        {
            var json = JsonSerializer.Serialize(message);
            var helper = new MongoPersistHelper<SaleDeletedEvent>(_mongoMessageService);
            await helper.PersistAsync(message, message.EventId, message.EventDateTime);
        }
    }
}
