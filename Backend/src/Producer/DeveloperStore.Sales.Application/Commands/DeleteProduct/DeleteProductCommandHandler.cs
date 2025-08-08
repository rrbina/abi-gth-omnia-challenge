using DeveloperStore.Sales.Application.Services.Products;
using MediatR;
using DeveloperStore.Sales.Application.Common;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using System;

namespace DeveloperStore.Sales.Application.Sales.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Guid>
    {
        private readonly IProductService _productService;
        private readonly IEventPublisher _eventPublisher;

        public DeleteProductCommandHandler(IProductService productService, IEventPublisher eventPublisher)
        {
            _productService = productService;
            _eventPublisher = eventPublisher;
        }

        public async Task<Guid> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _productService.DeleteProductAsync(request.ProductId);

            Console.WriteLine($"[EVENTO] ProductDeleted - ProductId: {request.ProductId}");
            var eventId = Guid.NewGuid();

            await _eventPublisher.PublishAsync(new ProductDeletedEvent
            {
                EventId = eventId,
                EventDateTime = DateTime.UtcNow,
                ProductId = request.ProductId,
                DeletedAt = DateTime.UtcNow
            });

            return eventId;
        }
    }
}