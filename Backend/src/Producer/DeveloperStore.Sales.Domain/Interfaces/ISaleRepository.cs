using DeveloperStore.Sales.Domain.Entities;

namespace DeveloperStore.Sales.Domain.Interfaces.Repositories
{
    public interface ISaleRepository
    {
        Task<Sale?> GetByIdAsync(Guid id);
        Task<IEnumerable<Sale>> GetAllAsync();
        Task AddAsync(Sale sale);
        Task UpdateAsync(Sale sale);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}