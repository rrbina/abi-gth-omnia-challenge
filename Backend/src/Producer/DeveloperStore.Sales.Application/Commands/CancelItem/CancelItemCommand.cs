using MediatR;
using System;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.CancelItem
{
    [ExcludeFromCodeCoverage]
    public class CancelItemCommand : IRequest<Guid>
    {
        public Guid SaleNumber { get; set; }
        public Guid ItemId { get; set; }

        public CancelItemCommand(Guid saleId, Guid itemId)
        {
            SaleNumber = saleId;
            ItemId = itemId;
        }

        public CancelItemCommand()
        {
                
        }
    }
}