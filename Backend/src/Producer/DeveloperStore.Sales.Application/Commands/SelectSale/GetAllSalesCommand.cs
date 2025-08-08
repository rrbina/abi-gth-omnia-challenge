using DeveloperStore.Sales.Application.DTOs;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.SelectSale
{
    [ExcludeFromCodeCoverage]
    public class GetAllSalesCommand : IRequest<IEnumerable<SaleDto>>
    {
    }
}