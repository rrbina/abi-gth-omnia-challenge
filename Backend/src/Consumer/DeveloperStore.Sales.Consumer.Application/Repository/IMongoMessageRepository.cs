using DeveloperStore.Sales.Consumer.Domain.Entities;

namespace DeveloperStore.Sales.Consumer.Application.Contracts
{
    public interface IMongoMessageRepository
    {
        Task<MongoMessage> AddAsync(MongoMessage message);
        Task<MongoMessage?> GetByIdAsync(Guid id);
        Task<IEnumerable<MongoMessage>> GetAllAsync();
        Task<MongoMessage> UpdateAsync(MongoMessage message);
        Task<MongoMessage> DeleteAsync(Guid id);
    }
}