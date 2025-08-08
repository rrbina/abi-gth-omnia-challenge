using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Interfaces.Repositories;
using DeveloperStore.Sales.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Infrastructure.Repositories
{
    [ExcludeFromCodeCoverage]
    public class SaleRepository : ISaleRepository
    {
        private readonly SalesDbContext _context;

        public SaleRepository(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<Sale?> GetByIdAsync(Guid saleNumber)
        {
            return await _context.Set<Sale>()
                .Include(s => s.Customer)
                .Include(s => s.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(s => s.SaleNumber == saleNumber);

        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _context.Set<Sale>()
                .Include(s => s.Customer)
                .Include(s => s.Items)
                    .ThenInclude(i => i.Product)
                .ToListAsync();

        }

        public async Task AddAsync(Sale sale)
        {
            await _context.Set<Sale>().AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sale sale)
        {
            _context.Set<Sale>().Update(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid saleNumber)
        {
            var sale = await GetByIdAsync(saleNumber);
            if (sale != null)
            {
                _context.Set<Sale>().Remove(sale);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(Guid saleNumber)
        {
            return await _context.Set<Sale>().AnyAsync(s => s.SaleNumber == saleNumber);
        }
    }
}