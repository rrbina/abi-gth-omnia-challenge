using DeveloperStore.Sales.Consumer.Application.Contracts;
using DeveloperStore.Sales.Consumer.Domain.Entities;

namespace DeveloperStore.Sales.Consumer.Application.Services.Confirmation
{
    public class MongoConfirmationService : IMongoConfirmationService
    {
        private readonly IMongoMessageRepository _repository;

        public MongoConfirmationService(IMongoMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<MongoMessage?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);            
        }
    }
}