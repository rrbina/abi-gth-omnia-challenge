using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Sales.Commands.CreateCustomer;
using DeveloperStore.Sales.Application.Sales.Commands.UpdateCustomer;

namespace DeveloperStore.Sales.Application.Services.Customers
{
    public interface ICustomerService
    {
        Task<CustomerDto?> GetCustomerByIdAsync(Guid id);
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerCommand command);
        Task<CustomerDto> UpdateCustomerAsync(UpdateCustomerCommand command);
        Task<CustomerDto> DeleteCustomerAsync(Guid id);
    }
}