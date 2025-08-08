using DeveloperStore.Sales.Application.Services.Customers;
using MediatR;
using DeveloperStore.Sales.Application.Common;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using System;

namespace DeveloperStore.Sales.Application.Sales.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly ICustomerService _customerService;
        private readonly IEventPublisher _eventPublisher;

        public CreateCustomerCommandHandler(ICustomerService customerService, IEventPublisher eventPublisher)
        {
            _customerService = customerService;
            _eventPublisher = eventPublisher;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customerDto = await _customerService.CreateCustomerAsync(request);

            Console.WriteLine($"[EVENTO] CustomerCreated - CustomerId: {customerDto.Id}");
            var eventId = Guid.NewGuid();

            var customerCreatedEvent = new CustomerCreatedEvent
            {
                EventId = eventId,
                EventDateTime = DateTime.UtcNow,
                CustomerId = customerDto.Id,
                CreatedAt = DateTime.UtcNow
            };
            await _eventPublisher.PublishAsync(customerCreatedEvent);

            return eventId;
        }
    }
}