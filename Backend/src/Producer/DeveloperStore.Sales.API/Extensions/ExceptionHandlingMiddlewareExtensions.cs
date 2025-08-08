using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.API.Middlewares;

[ExcludeFromCodeCoverage]
public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseDeveloperStoreExceptionHandling(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}