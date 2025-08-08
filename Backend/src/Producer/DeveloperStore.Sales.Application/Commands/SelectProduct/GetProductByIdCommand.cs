using DeveloperStore.Sales.Application.DTOs;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.SelectProduct
{
    [ExcludeFromCodeCoverage]
    public class GetProductByIdCommand : IRequest<ProductDto?>
    {
        public Guid ProductId { get; }

        public GetProductByIdCommand(Guid productId)
        {
            ProductId = productId;
        }
    }
}