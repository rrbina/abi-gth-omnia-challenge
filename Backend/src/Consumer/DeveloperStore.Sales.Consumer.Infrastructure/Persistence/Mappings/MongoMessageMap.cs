using DeveloperStore.Sales.Consumer.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Consumer.Infrastructure.Persistence.Mappings
{
    [ExcludeFromCodeCoverage]
    public static class MongoMessageMap
    {
        public static void Register()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(MongoMessage)))
            {
                BsonClassMap.RegisterClassMap<MongoMessage>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdMember(x => x.Id)
                      .SetSerializer(new GuidSerializer(BsonType.Binary));
                });
            }
        }
    }
}