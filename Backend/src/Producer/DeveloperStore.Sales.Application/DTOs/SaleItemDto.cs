using DeveloperStore.Sales.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class SaleItemDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }

        public SaleItemDto()
        {

        }

        public SaleItemDto(SaleItem saleItem)
        {
            Id = saleItem.Id;
            ProductId = saleItem.ProductId;
            UnitPrice = saleItem.Product.UnitPrice;
            Quantity = saleItem.Quantity;
            Discount = saleItem.Discount;
        }
    }
}