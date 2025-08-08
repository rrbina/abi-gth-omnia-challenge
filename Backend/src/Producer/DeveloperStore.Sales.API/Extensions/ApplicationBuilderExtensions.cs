using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseDeveloperStorePipeline(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeveloperStore API");
                c.RoutePrefix = string.Empty;
            });

            //app.UseHttpsRedirection();            

            return app;
        }
    }
}