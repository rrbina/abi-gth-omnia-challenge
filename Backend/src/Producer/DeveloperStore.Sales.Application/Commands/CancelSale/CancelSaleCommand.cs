using MediatR;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.CancelSale
{
    [ExcludeFromCodeCoverage]
    public class CancelSaleCommand : IRequest<Guid>
    {
        public Guid SaleNumber { get; set; }

        public CancelSaleCommand(Guid saleId)
        {
            SaleNumber = saleId;
        }
        public CancelSaleCommand()
        {
                
        }
    }
}