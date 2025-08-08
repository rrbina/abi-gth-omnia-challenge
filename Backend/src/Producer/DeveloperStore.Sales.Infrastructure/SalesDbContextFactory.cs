using DeveloperStore.Sales.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Infrastructure
{
    [ExcludeFromCodeCoverage]
    public class SalesDbContextFactory : IDesignTimeDbContextFactory<SalesDbContext>
    {
        public SalesDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            if (environment == "Development")
                configurationBuilder.AddJsonFile("appsettings.Development.json", optional: false);
            else
                configurationBuilder.AddJsonFile("appsettings.json", optional: false);

            configurationBuilder.AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<SalesDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new SalesDbContext(optionsBuilder.Options);
        }
    }
}