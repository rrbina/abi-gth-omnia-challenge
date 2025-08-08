using DeveloperStore.Sales.Application.Common;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using MediatR;
using DeveloperStore.Sales.Application.Services.Sales;
using System;

namespace DeveloperStore.Sales.Application.Sales.Commands.UpdateSale
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Guid>
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ISaleService _saleService;

        public UpdateSaleCommandHandler(IEventPublisher eventPublisher, ISaleService saleService)
        {
            _eventPublisher = eventPublisher;
            _saleService = saleService;
        }

        public async Task<Guid> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleService.UpdateSaleAsync(request);

            Console.WriteLine($"[EVENTO] SaleModified - SaleId: {sale.SaleNumber}");
            var eventId = Guid.NewGuid();

            await _eventPublisher.PublishAsync(new SaleModifiedEvent
            {
                EventId = eventId,
                EventDateTime = DateTime.UtcNow,
                SaleNumber = sale.SaleNumber ?? Guid.Empty,
                ModifiedDate = DateTime.UtcNow,
                NewTotalAmount = sale.TotalAmount
            });

            return eventId;
        }
    }
}