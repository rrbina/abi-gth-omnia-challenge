using DeveloperStore.Sales.Consumer.Domain.Entities;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Consumer.Infrastructure.Persistence.Mappings
{
    [ExcludeFromCodeCoverage]
    public static class MongoMessageIndex
    {
        public static void Configure(IMongoCollection<MongoMessage> collection)
        {
            var indexKeys = Builders<MongoMessage>.IndexKeys.Descending(m => m.Timestamp);
            var indexModel = new CreateIndexModel<MongoMessage>(indexKeys);
            collection.Indexes.CreateOne(indexModel);
        }
    }
}