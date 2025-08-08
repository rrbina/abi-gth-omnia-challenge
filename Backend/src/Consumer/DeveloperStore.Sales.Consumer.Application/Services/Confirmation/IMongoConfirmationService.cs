using DeveloperStore.Sales.Consumer.Domain.Entities;

namespace DeveloperStore.Sales.Consumer.Application.Services.Confirmation
{
    public interface IMongoConfirmationService
    {
        Task<MongoMessage?> GetByIdAsync(Guid id);
    }
}