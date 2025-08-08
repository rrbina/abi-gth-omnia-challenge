using DeveloperStore.Sales.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class DatabaseServiceExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            //var dbSettings = configuration.GetSection("DatabaseSettings");
            //var connectionString = $"Host={dbSettings["Host"]};Port={dbSettings["Port"]};Database={dbSettings["Database"]};Username={dbSettings["Username"]};Password={dbSettings["Password"]}";
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<SalesDbContext>(options =>
                options.UseNpgsql(connectionString));

            return services;
        }
    }
}