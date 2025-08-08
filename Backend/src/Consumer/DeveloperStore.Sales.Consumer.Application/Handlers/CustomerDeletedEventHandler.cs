using DeveloperStore.Sales.Consumer.Application.Helpers;
using DeveloperStore.Sales.Consumer.Application.Services.MongoMessages;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using Rebus.Handlers;
using System.Text.Json;

namespace DeveloperStore.Sales.Consumer.Application.Handler
{
    public class CustomerDeletedEventHandler : IHandleMessages<CustomerDeletedEvent>
    {
        private readonly IMongoMessageService _mongoMessageService;

        public CustomerDeletedEventHandler(IMongoMessageService mongoMessageService)
        {
            _mongoMessageService = mongoMessageService;
        }

        public async Task Handle(CustomerDeletedEvent message)
        {
            try 
            {
                var json = JsonSerializer.Serialize(message);
                var helper = new MongoPersistHelper<CustomerDeletedEvent>(_mongoMessageService);
                await helper.PersistAsync(message, message.EventId, message.EventDateTime);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }            
        }
    }
}