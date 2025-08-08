using Rebus.Config;
using DeveloperStore.Sales.Consumer.Application.Handler;
using DeveloperStore.Sales.Messages.IntegrationEvents;
using Rebus.Retry.Simple;
using Rebus.Routing.TypeBased;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Consumer.API.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class RebusServiceExtensions
    {
        public static IServiceCollection AddRebusConfiguration(this IServiceCollection services, 
            IConfiguration configuration)
        {
            var connectionString = configuration["RabbitMq:ConnectionString"];
            var queueName = configuration["RabbitMq:QueueName"];

            services.AddRebus(configure =>

                configure
                    .Transport(t => t.UseRabbitMq(connectionString, queueName))
                .Routing(r => r.TypeBased()
                        .Map<CustomerCreatedEvent>(queueName)
                        .Map<CustomerDeletedEvent>(queueName)
                        .Map<CustomerUpdatedEvent>(queueName)
                        .Map<ItemCancelledEvent>(queueName)
                        .Map<ProductCreatedEvent>(queueName)
                        .Map<ProductDeletedEvent>(queueName)
                        .Map<ProductUpdatedEvent>(queueName)
                        .Map<SaleCancelledEvent>(queueName)
                        .Map<SaleCreatedEvent>(queueName)
                        .Map<SaleDeletedEvent>(queueName)
                        .Map<SaleModifiedEvent>(queueName)
                    )
                    .Options(o =>
                    {
                        o.SetNumberOfWorkers(1);
                        o.SetMaxParallelism(1);
                        o.RetryStrategy(maxDeliveryAttempts: 3);
                    })
            );


            services.AutoRegisterHandlersFromAssemblyOf<HandlerMarker>();

            return services;
        }

        public static void UseLifetimeOnStartup(this WebApplication app)
        {
            app.Lifetime.ApplicationStarted.Register(() =>
            {
                Console.WriteLine("Consumer iniciado e ouvindo eventos...");
            });
            app.Lifetime.ApplicationStopping.Register(() =>
            {
                Console.WriteLine("Consumer está sendo finalizado...");
            });
        }
    }
}
