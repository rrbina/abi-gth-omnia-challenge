using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Sales.Commands.CreateCustomer;
using DeveloperStore.Sales.Application.Sales.Commands.UpdateCustomer;
using DeveloperStore.Sales.Application.Services.Customers;
using DeveloperStore.Sales.Domain.Entities;
using DeveloperStore.Sales.Domain.Exceptions;
using DeveloperStore.Sales.Domain.Interfaces.Repositories;

namespace DeveloperStore.Sales.Application.Services.Sales
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerCommand command)
        {
            var customerExists = await _customerRepository.ExistsByNameAsync(command.CustomerName);
            if (customerExists)
            {
                throw new DeveloperStoreException($"Customer with name '{command.CustomerName}' already exists.");
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                CustomerName = command.CustomerName
            };

            await _customerRepository.AddAsync(customer);
            return new CustomerDto(customer);
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(c => new CustomerDto(c));
        }

        public async Task<CustomerDto?> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return customer is null ? null : new CustomerDto(customer);
        }

        public async Task<CustomerDto> UpdateCustomerAsync(UpdateCustomerCommand command)
        {
            var customer = await _customerRepository.GetByIdAsync(command.Id);
            if (customer is null)
                throw new Exception("Cliente não encontrado.");

            customer.CustomerName = command.CustomerName;
            await _customerRepository.UpdateAsync(customer);

            return new CustomerDto(customer);
        }

        public async Task<CustomerDto> DeleteCustomerAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer is null)
                throw new DeveloperStoreException("Cliente não encontrado.");

            await _customerRepository.DeleteAsync(id);
            return new CustomerDto(customer);
        }
    }
}