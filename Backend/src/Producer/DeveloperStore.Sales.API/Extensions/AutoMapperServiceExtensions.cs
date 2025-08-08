using DeveloperStore.Sales.API.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class AutoMapperServiceExtensions
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            var loggerFactory = LoggerFactory.Create(loggingBuilder => {
                loggingBuilder.AddConsole();
            });
            var mapperConfig = AutoMapperFactory.CreateMapperConfiguration(loggerFactory);
            services.AddSingleton(mapperConfig.CreateMapper());
            return services;
        }
    }
}