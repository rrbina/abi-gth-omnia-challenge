using DeveloperStore.Sales.Application.Services.Products;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using DeveloperStore.Sales.Application.Common;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using System;

namespace DeveloperStore.Sales.Application.Sales.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Guid>
    {
        private readonly IProductService _productService;
        private readonly IEventPublisher _eventPublisher;

        public UpdateProductCommandHandler(IProductService productService, IEventPublisher eventPublisher)
        {
            _productService = productService;
            _eventPublisher = eventPublisher;
        }

        public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await _productService.UpdateProductAsync(request);

            Console.WriteLine($"[EVENTO] ProductUpdated - ProductId: {request.Id}");
            var eventId = Guid.NewGuid();

            await _eventPublisher.PublishAsync(new ProductUpdatedEvent
            {
                EventId = eventId,
                EventDateTime = DateTime.UtcNow,
                ProductId = request.Id,
                UpdatedAt = DateTime.UtcNow
            });

            return eventId;
        }
    }
}