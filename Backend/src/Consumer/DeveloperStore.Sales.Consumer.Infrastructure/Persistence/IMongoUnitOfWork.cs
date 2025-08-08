using DeveloperStore.Sales.Consumer.Domain.Entities;

namespace DeveloperStore.Sales.Consumer.Infrastructure.Persistence
{
    public interface IMongoUnitOfWork
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        Task AddAsync(MongoMessage message);
        Task<MongoMessage?> GetByIdAsync(Guid id);
        Task<IEnumerable<MongoMessage>> GetAllAsync();
        Task UpdateAsync(MongoMessage message);
        Task DeleteAsync(Guid id);
    }
}