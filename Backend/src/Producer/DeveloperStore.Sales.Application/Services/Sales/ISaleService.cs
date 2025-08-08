using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Sales.Commands.CreateSale;
using DeveloperStore.Sales.Application.Sales.Commands.UpdateSale;
namespace DeveloperStore.Sales.Application.Services.Sales
{
    public interface ISaleService
    {
        Task<SaleDto?> GetSaleByIdAsync(Guid id);
        Task<IEnumerable<SaleDto>> GetAllSaleAsync();
        Task<SaleDto> UpdateSaleAsync(UpdateSaleCommand command);
        Task<SaleDto> DeleteSaleAsync(Guid id);
        Task<CancelSaleItemDto> CancelItemAsync(Guid saleId, Guid itemId);
        Task<SaleDto> CancelSaleAsync(Guid saleId);
        Task<SaleDto> CreateSaleAsync(CreateSaleCommand command);
    }
}