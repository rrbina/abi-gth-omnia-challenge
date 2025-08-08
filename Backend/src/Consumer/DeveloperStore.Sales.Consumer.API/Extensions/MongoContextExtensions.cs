using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using DeveloperStore.Sales.Consumer.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using DeveloperStore.Sales.Consumer.Infrastructure.Persistence;

public static class MongoContextExtensions
{
    [ExcludeFromCodeCoverage]
    public static IServiceCollection AddMongoContext(this IServiceCollection services, IConfiguration configuration)
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(MongoMessage)))
        {
            BsonClassMap.RegisterClassMap<MongoMessage>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.Id).SetSerializer(new GuidSerializer(MongoDB.Bson.GuidRepresentation.Standard));
                cm.MapMember(c => c.Message);
                cm.MapMember(c => c.Timestamp);
            });
        }

        services.AddSingleton<MongoDbContext>(provider =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
            var connectionString = config["MongoSettings:ConnectionString"]!;
            var databaseName = config["MongoSettings:DatabaseName"]!;
            return new MongoDbContext(connectionString, databaseName);
        });

        return services;
    }
}