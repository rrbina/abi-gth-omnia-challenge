using DeveloperStore.Sales.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }

        public CustomerDto() { }

        public CustomerDto(Customer customer)
        {
            Id = customer.Id;
            CustomerName = customer.CustomerName;
        }
    }
}