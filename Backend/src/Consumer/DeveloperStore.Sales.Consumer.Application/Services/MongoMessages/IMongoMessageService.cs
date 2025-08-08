using DeveloperStore.Sales.Consumer.Domain.Entities;

namespace DeveloperStore.Sales.Consumer.Application.Services.MongoMessages
{
    public interface IMongoMessageService
    {
        Task<MongoMessage> PersistMessageAsync(string message, Guid id, DateTime timestamp);
        Task<MongoMessage?> GetByIdAsync(Guid id);
        Task<IEnumerable<MongoMessage>> GetAllAsync();
        Task DeleteAsync(Guid id);
    }
}