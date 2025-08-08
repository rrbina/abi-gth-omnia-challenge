using DeveloperStore.Sales.Consumer.Application.Contracts;
using DeveloperStore.Sales.Consumer.Domain.Entities;

namespace DeveloperStore.Sales.Consumer.Application.Services.MongoMessages
{
    public class MongoMessageService : IMongoMessageService
    {
        private readonly IMongoMessageRepository _mongoMessageRepository;

        public MongoMessageService(IMongoMessageRepository _mongoMessageRepository)
        {
            this._mongoMessageRepository = _mongoMessageRepository;
        }

        public async Task<MongoMessage> PersistMessageAsync(string message, Guid id, DateTime timestamp)
        {
            var dto = new MongoMessage
            {
                Id = id,
                Message = message,
                Timestamp = timestamp
            };

            return await _mongoMessageRepository.AddAsync(dto);
        }

        public async Task<MongoMessage?> GetByIdAsync(Guid id)
        {
            return await _mongoMessageRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<MongoMessage>> GetAllAsync()
        {
            return await _mongoMessageRepository.GetAllAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _mongoMessageRepository.DeleteAsync(id);
        }       
    }
}