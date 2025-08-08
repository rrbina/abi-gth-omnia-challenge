using DeveloperStore.Sales.Application.DTOs;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.SelectCustomer
{
    [ExcludeFromCodeCoverage]
    public class GetAllCustomersCommand : IRequest<IEnumerable<CustomerDto>>
    {
    }
}