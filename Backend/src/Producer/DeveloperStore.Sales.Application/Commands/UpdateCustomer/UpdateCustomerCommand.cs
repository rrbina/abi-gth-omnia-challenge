using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.Sales.Commands.UpdateCustomer
{
    [ExcludeFromCodeCoverage]
    public class UpdateCustomerCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }

        public UpdateCustomerCommand() { }

        public UpdateCustomerCommand(Guid id, string customerName)
        {
            Id = id;
            CustomerName = customerName;
        }
    }
}