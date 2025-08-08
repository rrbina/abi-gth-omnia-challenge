using DeveloperStore.Sales.Application.Common;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using MediatR;
using DeveloperStore.Sales.Application.Services.Sales;
using System;

namespace DeveloperStore.Sales.Application.Sales.Commands.CancelItem
{
    public class CancelItemCommandHandler : IRequestHandler<CancelItemCommand, Guid>
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ISaleService _saleService;

        public CancelItemCommandHandler(IEventPublisher eventPublisher, ISaleService saleService)
        {
            _eventPublisher = eventPublisher;
            _saleService = saleService;
        }

        public async Task<Guid> Handle(CancelItemCommand request, CancellationToken cancellationToken)
        {
            var cancelItemDto = await _saleService.CancelItemAsync(request.SaleNumber, request.ItemId);

            Console.WriteLine($"[EVENTO] ItemCancelled - SaleId: {cancelItemDto.Sale.SaleNumber}, ItemId: {cancelItemDto.Item.ProductId}");
            var eventId = Guid.NewGuid();

            await _eventPublisher.PublishAsync(new ItemCancelledEvent
            {
                EventId = eventId,
                EventDateTime = DateTime.UtcNow,
                SaleNumber = cancelItemDto.Sale.SaleNumber ?? Guid.Empty,
                ItemId = cancelItemDto.Item.ProductId,
                CancelledAt = DateTime.UtcNow
            });

            return eventId;
        }
    }
}