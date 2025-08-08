using DeveloperStore.Sales.Consumer.Application.Services.Confirmation;
using DeveloperStore.Sales.Consumer.Application.Services.MongoMessages;
using DeveloperStore.Sales.Consumer.Infrastructure.Persistence;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Consumer.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class MongoServicesExtensions
    {
        public static IServiceCollection AddMongoServices(this IServiceCollection services)
        {
            services.AddScoped<IMongoUnitOfWork, MongoUnitOfWork>();
            services.AddScoped<IMongoMessageService, MongoMessageService>();
            services.AddScoped<IMongoConfirmationService, MongoConfirmationService>();

            return services;
        }
    }
}