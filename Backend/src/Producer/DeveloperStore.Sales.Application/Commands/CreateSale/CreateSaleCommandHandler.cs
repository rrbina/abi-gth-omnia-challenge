using DeveloperStore.Sales.Application.Common;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using MediatR;
using DeveloperStore.Sales.Application.Services.Sales;
using System;

namespace DeveloperStore.Sales.Application.Sales.Commands.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ISaleService _saleService;

        public CreateSaleCommandHandler(IEventPublisher eventPublisher, ISaleService saleService)
        {
            _eventPublisher = eventPublisher;
            _saleService = saleService;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleService.CreateSaleAsync(request);

            Console.WriteLine($"[EVENTO] SaleCreated - SaleId: {sale.SaleNumber}");
            var eventId = Guid.NewGuid();

            await _eventPublisher.PublishAsync<SaleCreatedEvent>(new SaleCreatedEvent
            {
                EventId = eventId,
                EventDateTime = sale.SaleDate,
                CustomerName = sale.CustomerName,
                TotalAmount = sale.TotalAmount,
                TotalDiscount = sale.Discount ?? 0.0m
            });

            return eventId;
        }
    }
}