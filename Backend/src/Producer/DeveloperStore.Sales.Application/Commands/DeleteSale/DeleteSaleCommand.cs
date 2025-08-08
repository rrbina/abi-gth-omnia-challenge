using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.DeleteSale
{
    [ExcludeFromCodeCoverage]
    public class DeleteSaleCommand : IRequest<Guid>
    {
        public Guid SaleNumber { get; }

        public DeleteSaleCommand(Guid id)
        {
            SaleNumber = id;
        }
    }
}