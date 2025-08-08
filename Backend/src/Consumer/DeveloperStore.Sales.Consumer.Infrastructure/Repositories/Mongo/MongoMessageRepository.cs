using DeveloperStore.Sales.Consumer.Application.Contracts;
using DeveloperStore.Sales.Consumer.Domain.Entities;
using DeveloperStore.Sales.Consumer.Infrastructure.Persistence;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Consumer.Infrastructure.Repositories.Mongo
{
    [ExcludeFromCodeCoverage]
    public class MongoMessageRepository : IMongoMessageRepository
    {
        private readonly IMongoCollection<MongoMessage> _collection;
        private readonly MongoDbContext _context;

        public MongoMessageRepository(MongoDbContext context)
        {
            _context = context;
            _collection = _context.GetCollection<MongoMessage>("MongoMessages");
        }

        public async Task<MongoMessage> AddAsync(MongoMessage mongoMessage)
        {
            try
            {
                var existingMessage = await _collection.Find(m => m.Id == mongoMessage.Id).FirstOrDefaultAsync();

                if (existingMessage != null)
                {
                    return new MongoMessage { Message = "A mensagem já existe." };
                }
                return new MongoMessage { Message = "Operação Realizada Com Sucesso" };
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return new MongoMessage { Message = ex.Message };
            }            
        }

        public async Task<MongoMessage?> GetByIdAsync(Guid id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();            
        }

        public async Task<IEnumerable<MongoMessage>> GetAllAsync()
        {
            var result = await _collection.Find(_ => true).ToListAsync();
            return result.Select(entity => new MongoMessage
            {
                Id = entity.Id,
                Message = entity.Message,
                Timestamp = entity.Timestamp
            });
        }

        public async Task<MongoMessage> UpdateAsync(MongoMessage mongoMessage)
        {
            await _collection.ReplaceOneAsync(x => x.Id == mongoMessage.Id, mongoMessage);
            return new MongoMessage { Message = "Operação Realizada Com Sucesso" };
        }

        public async Task<MongoMessage> DeleteAsync(Guid id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
            return new MongoMessage { Message = "Operação Realizada Com Sucesso" };
        }
    }
}