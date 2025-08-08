using FluentValidation.AspNetCore;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class FluentValidationExtensions
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();

            return services;
        }
    }
}
