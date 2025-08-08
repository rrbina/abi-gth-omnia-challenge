using DeveloperStore.Sales.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class CancelSaleDto
    {
        public Sale sale { get; set; }
        public CancelSaleDto(Sale _sale)
        {
            sale = _sale;
        }
    }
}
