using DeveloperStore.Sales.Application.DTOs;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.CreateSale
{
    [ExcludeFromCodeCoverage]
    public class CreateSaleCommand : IRequest<Guid>
    {
        public SaleDto SaleDto { get; set; }

        public CreateSaleCommand()
        {
        }
    }    
}
