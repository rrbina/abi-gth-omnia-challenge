using DeveloperStore.Sales.Application.Helpers;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class CustomValidationServiceExtensions
    {
        public static IServiceCollection AddCustomValidation(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ApplicationMarker).Assembly;
            services.AddValidatorsFromAssembly(applicationAssembly);
            return services;
        }
    }
}
