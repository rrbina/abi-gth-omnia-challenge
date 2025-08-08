using System.Text.Json;
using DeveloperStore.Sales.Consumer.Application.Services.MongoMessages;
using DeveloperStore.Sales.Consumer.Domain.Entities;

namespace DeveloperStore.Sales.Consumer.Application.Helpers
{
    public class MongoPersistHelper<T>
    {
        private readonly IMongoMessageService? _mongoMessageService;

        public MongoPersistHelper(IMongoMessageService? mongoMessageService)
        {
            if (mongoMessageService != null)
                _mongoMessageService = mongoMessageService;
        }

        public async Task<MongoMessage> PersistAsync(T message, Guid eventId, DateTime eventDateTime)
        {
            if (_mongoMessageService == null)
               return null;

            var json = JsonSerializer.Serialize(message);
            return await _mongoMessageService.PersistMessageAsync(json, eventId, eventDateTime);
        }
    }
}