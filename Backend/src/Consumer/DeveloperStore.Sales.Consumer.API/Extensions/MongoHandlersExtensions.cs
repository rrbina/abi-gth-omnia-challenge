using DeveloperStore.Sales.Consumer.Application.Handler;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Consumer.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class MongoHandlersExtensions
    {
        public static IServiceCollection AddMongoHandlers(this IServiceCollection services)
        {
            services.AddScoped<MongoConfirmationByIdHandler>();
            return services;
        }
    }
}