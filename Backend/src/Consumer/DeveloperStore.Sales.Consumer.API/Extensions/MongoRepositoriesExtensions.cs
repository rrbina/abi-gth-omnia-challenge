using DeveloperStore.Sales.Consumer.Application.Contracts;
using DeveloperStore.Sales.Consumer.Infrastructure.Repositories.Mongo;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Consumer.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class MongoRepositoriesExtensions
    {
        public static IServiceCollection AddMongoRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMongoMessageRepository, MongoMessageRepository>();
            return services;
        }
    }
}