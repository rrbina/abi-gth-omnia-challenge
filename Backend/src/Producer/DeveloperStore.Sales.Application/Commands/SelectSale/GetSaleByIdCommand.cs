using DeveloperStore.Sales.Application.DTOs;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.SelectSale
{
    [ExcludeFromCodeCoverage]
    public class GetSaleByIdCommand : IRequest<SaleDto?>
    {
        public Guid SaleNumber { get; }

        public GetSaleByIdCommand(Guid saleNumber)
        {
            SaleNumber = saleNumber;
        }
    }
}