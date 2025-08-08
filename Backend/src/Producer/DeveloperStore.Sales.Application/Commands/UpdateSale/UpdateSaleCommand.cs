using DeveloperStore.Sales.Application.DTOs;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.UpdateSale
{
    [ExcludeFromCodeCoverage]
    public class UpdateSaleCommand : IRequest<Guid>
    {
        public Guid SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }

        public Guid BranchId { get; set; }
        public string BranchName { get; set; }

        public List<SaleItemDto> Items { get; set; } = new();
    }    
}
