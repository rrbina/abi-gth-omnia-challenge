using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }

        public ProductDto() { }

        public ProductDto(Domain.Entities.Product product)
        {
            Id = product.Id;
            ProductName = product.ProductName;
            UnitPrice = product.UnitPrice;
        }
    }
}