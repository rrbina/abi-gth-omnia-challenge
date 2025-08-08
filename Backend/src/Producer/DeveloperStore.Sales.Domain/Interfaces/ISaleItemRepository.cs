using DeveloperStore.Sales.Domain.Entities;

namespace DeveloperStore.Sales.Domain.Interfaces.Repositories
{
    public interface ISaleItemRepository
    {
        Task<SaleItem?> GetByIdAsync(Guid id);
        Task<IEnumerable<SaleItem>> GetBySaleIdWithCustomerAsync(Guid saleId);
        Task<List<SaleItem>> GetBySaleNumberAsync(Guid saleNumber);        
        Task AddAsync(SaleItem item);
        Task UpdateAsync(SaleItem item);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}