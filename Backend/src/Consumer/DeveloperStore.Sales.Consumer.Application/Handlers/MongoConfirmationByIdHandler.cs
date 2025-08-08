using DeveloperStore.Sales.Consumer.Application.Services.Confirmation;
using DeveloperStore.Sales.Consumer.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Consumer.Application.Handler
{
    [ExcludeFromCodeCoverage]
    public class MongoConfirmationByIdHandler
    {
        private readonly IMongoConfirmationService _service;

        public MongoConfirmationByIdHandler(IMongoConfirmationService service)
        {
            _service = service;
        }

        public async Task<MongoMessage?> HandleAsync(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }
    }
}