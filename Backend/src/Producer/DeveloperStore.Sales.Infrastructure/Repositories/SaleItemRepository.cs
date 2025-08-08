using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Interfaces.Repositories;
using DeveloperStore.Sales.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class SaleItemRepository : ISaleItemRepository
    {
        private readonly SalesDbContext _context;

        public SaleItemRepository(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<SaleItem?> GetByIdAsync(Guid id)
        {
            return await _context.Set<SaleItem>()
                                 .Include(i => i.Sale)
                                      .ThenInclude(s => s.Customer)
                                 .Include(i => i.Product)
                                 .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<SaleItem>> GetBySaleIdWithCustomerAsync(Guid saleId)
        {
            return await _context.Set<SaleItem>()
                                 .Where(i => i.SaleId == saleId)
                                  .Include(i => i.Sale)
                                      .ThenInclude(s => s.Customer)
                                 .Include(i => i.Product)
                                 .ToListAsync();
        }

        public async Task<List<SaleItem>> GetBySaleNumberAsync(Guid saleNumber)
        {
            var sale = await _context.Set<Sale>()
                .Include(s => s.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(s => s.SaleNumber == saleNumber);

            if (sale == null)
                return new List<SaleItem>();

            return sale.Items
                .Where(i => !i.IsCancelled)
                .ToList();
        }


        public async Task AddAsync(SaleItem item)
        {
            await _context.Set<SaleItem>().AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SaleItem item)
        {
            _context.Set<SaleItem>().Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _context.Set<SaleItem>().Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Set<SaleItem>().AnyAsync(i => i.Id == id);
        }
    }
}