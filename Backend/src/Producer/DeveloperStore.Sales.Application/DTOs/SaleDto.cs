using DeveloperStore.Sales.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class SaleDto
    {
        public Guid? SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string BranchName { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<SaleItemDto> Items { get; set; }
        public decimal? Discount { get; set; }

        public SaleDto()
        {

        }
        public SaleDto(Sale sale)
        {
            SaleNumber = sale.SaleNumber;
            SaleDate = sale.SaleDate;
            CustomerId = sale.CustomerId;
            CustomerName = sale.Customer.CustomerName;
            BranchName = sale.BranchName;
            TotalAmount = sale.TotalAmount;
            Items = sale.Items.Select(item => new SaleItemDto(item)).ToList();
            Discount = sale.TotalDiscount;
        }
    }
}