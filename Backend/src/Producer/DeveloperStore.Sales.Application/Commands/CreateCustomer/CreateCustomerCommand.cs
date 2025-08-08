using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.CreateCustomer
{
    [ExcludeFromCodeCoverage]
    public class CreateCustomerCommand : IRequest<Guid>
    {
        public string CustomerName { get; set; }

        public CreateCustomerCommand() { }

        public CreateCustomerCommand(string customerName)
        {
            CustomerName = customerName;
        }
    }
}