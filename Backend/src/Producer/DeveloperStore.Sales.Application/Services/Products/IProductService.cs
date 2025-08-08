using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Sales.Commands.CreateProduct;
using DeveloperStore.Sales.Application.Sales.Commands.UpdateProduct;

namespace DeveloperStore.Sales.Application.Services.Products
{
    public interface IProductService
    {
        Task<ProductDto?> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> UpdateProductAsync(UpdateProductCommand command);
        Task<ProductDto> DeleteProductAsync(Guid id);
        Task<ProductDto> CreateProductAsync(CreateProductCommand command);
    }
}