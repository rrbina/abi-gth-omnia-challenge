using DeveloperStore.Sales.Application.Services.Products;
using MediatR;
using DeveloperStore.Sales.Application.Common;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using System;

namespace DeveloperStore.Sales.Application.Sales.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductService _productService;
        private readonly IEventPublisher _eventPublisher;

        public CreateProductCommandHandler(IProductService productService, IEventPublisher eventPublisher)
        {
            _productService = productService;
            _eventPublisher = eventPublisher;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productDto = await _productService.CreateProductAsync(request);

            Console.WriteLine($"[EVENTO] ProductCreated - ProductId: {productDto.Id}");
            var eventId = Guid.NewGuid();

            await _eventPublisher.PublishAsync(new ProductCreatedEvent
            {
                EventId = eventId,
                EventDateTime = DateTime.UtcNow,
                ProductId = productDto.Id,
                CreatedAt = DateTime.UtcNow
            });

            return eventId;
        }
    }
}