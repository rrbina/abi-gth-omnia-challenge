using DeveloperStore.Sales.Application.DTOs;
using DeveloperStore.Sales.Application.Sales.Commands.SelectCustomer;
using DeveloperStore.Sales.Application.Services.Customers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloperStore.Sales.Application.Sales.Handlers
{
    public class SelectCustomerHandler :
        IRequestHandler<GetAllCustomersCommand, IEnumerable<CustomerDto>>,
        IRequestHandler<GetCustomerByIdCommand, CustomerDto?>
    {
        private readonly ICustomerService _customerService;

        public SelectCustomerHandler(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersCommand request, CancellationToken cancellationToken)
        {
            return await _customerService.GetAllCustomersAsync();
        }

        public async Task<CustomerDto?> Handle(GetCustomerByIdCommand request, CancellationToken cancellationToken)
        {
            return await _customerService.GetCustomerByIdAsync(request.CustomerId);
        }
    }
}