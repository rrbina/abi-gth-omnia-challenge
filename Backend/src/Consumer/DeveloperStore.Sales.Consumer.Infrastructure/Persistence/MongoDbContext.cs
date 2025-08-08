using DeveloperStore.Sales.Consumer.Domain.Entities;
using DeveloperStore.Sales.Consumer.Infrastructure.Persistence.Mappings;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Consumer.Infrastructure.Persistence
{
    [ExcludeFromCodeCoverage]
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);

            //BsonSerializer.RegisterSerializer(new MongoDB.Bson.Serialization.Serializers.GuidSerializer(MongoDB.Bson.GuidRepresentation.Standard));

            var collections = _database.ListCollectionNames().ToList();

            if (!collections.Contains("DeveloperStoreDb"))
            {
                _database.CreateCollection("DeveloperStoreDb");
            }

            var collection = GetCollection<MongoMessage>("DeveloperStoreDb");

            MongoMessageMap.Register();
            MongoMessageIndex.Configure(collection);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _database.GetCollection<T>(name);
        }
    }
}