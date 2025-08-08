using DeveloperStore.Sales.Application.DTOs;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.SelectCustomer
{
    [ExcludeFromCodeCoverage]
    public class GetCustomerByIdCommand : IRequest<CustomerDto?>
    {
        public Guid CustomerId { get; }

        public GetCustomerByIdCommand(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}