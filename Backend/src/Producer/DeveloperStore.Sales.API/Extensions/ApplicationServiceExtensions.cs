using DeveloperStore.Sales.Application.Services.Customers;
using DeveloperStore.Sales.Application.Services.Products;
using DeveloperStore.Sales.Application.Services.Sales;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            return services;
        }
    }
}