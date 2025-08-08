using DeveloperStore.Sales.Application.DTOs;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.SelectProduct
{
    [ExcludeFromCodeCoverage]
    public class GetAllProductsCommand : IRequest<IEnumerable<ProductDto>>
    {
    }
}