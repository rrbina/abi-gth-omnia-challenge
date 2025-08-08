using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Sales.Commands.CreateProduct;
using DeveloperStore.Sales.Application.Sales.Commands.UpdateProduct;
using DeveloperStore.Sales.Application.Services.Products;
using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Exceptions;
using DeveloperStore.Sales.Domain.Interfaces.Repositories;

namespace DeveloperStore.Sales.Application.Services.Sales
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductCommand command)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = command.ProductName,
                UnitPrice = command.UnitPrice
            };

            await _productRepository.AddAsync(product);

            return new ProductDto(product);
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product is null ? null : new ProductDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDto(p));
        }

        public async Task<ProductDto> UpdateProductAsync(UpdateProductCommand command)
        {
            var product = await _productRepository.GetByIdAsync(command.Id);
            if (product is null)
                throw new DeveloperStoreException("Produto não encontrado.");

            product.ProductName = command.ProductName;
            product.UnitPrice = command.UnitPrice;

            await _productRepository.UpdateAsync(product);
            return new ProductDto(product);
        }

        public async Task<ProductDto> DeleteProductAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
                throw new DeveloperStoreException("Produto não encontrado.");

            await _productRepository.DeleteAsync(id);
            return new ProductDto(product);
        }
    }
}