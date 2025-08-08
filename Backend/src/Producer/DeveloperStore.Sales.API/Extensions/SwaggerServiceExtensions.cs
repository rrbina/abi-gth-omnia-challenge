using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DeveloperStore API",
                    Version = "v1"
                });
            });

            return services;
        }
    }
}
