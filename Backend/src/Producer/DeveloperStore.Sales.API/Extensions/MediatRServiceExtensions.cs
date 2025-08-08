using MediatR;
using DeveloperStore.Sales.Application.Behaviors;
using System.Diagnostics.CodeAnalysis;
using DeveloperStore.Sales.Application.Helpers;
namespace DeveloperStore.Sales.API.Extensions
{
    public static class MediatRServiceExtensions
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ApplicationMarker).Assembly;

            services.AddMediatR(applicationAssembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}