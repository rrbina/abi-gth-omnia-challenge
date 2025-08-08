using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.DeleteProduct
{
    [ExcludeFromCodeCoverage]
    public class DeleteProductCommand : IRequest<Guid>
    {
        public Guid ProductId { get; }

        public DeleteProductCommand(Guid productId)
        {
            ProductId = productId;
        }
    }
}