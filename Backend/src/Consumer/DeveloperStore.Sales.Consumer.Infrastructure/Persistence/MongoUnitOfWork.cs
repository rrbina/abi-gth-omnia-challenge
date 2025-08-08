using DeveloperStore.Sales.Consumer.Domain.Entities;
using MongoDB.Driver;
using System.Diagnostics.CodeAnalysis;

namespace DeveloperStore.Sales.Consumer.Infrastructure.Persistence
{
    [ExcludeFromCodeCoverage]
    public class MongoUnitOfWork : IMongoUnitOfWork
    {
        private readonly MongoDbContext _context;
        private IClientSessionHandle? _session;
        private readonly IMongoCollection<MongoMessage> _collection;

        public MongoUnitOfWork(MongoDbContext context)
        {
            _context = context;
            _collection = _context.GetCollection<MongoMessage>("MongoMessages");
        }

        public async Task BeginTransactionAsync()
        {
            //_session = await _context.StartSessionAsync();
            _session.StartTransaction();
        }

        public async Task CommitTransactionAsync()
        {
            if (_session != null)
            {
                await _session.CommitTransactionAsync();
                _session.Dispose();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_session != null)
            {
                await _session.AbortTransactionAsync();
                _session.Dispose();
            }
        }

        public async Task AddAsync(MongoMessage message)
        {
            if (_session != null)
                await _collection.InsertOneAsync(_session, message);
            else
                await _collection.InsertOneAsync(message);
        }

        public async Task<MongoMessage?> GetByIdAsync(Guid id)
        {
            var filter = Builders<MongoMessage>.Filter.Eq(m => m.Id, id);
            var result = await _collection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<MongoMessage>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task UpdateAsync(MongoMessage message)
        {
            var filter = Builders<MongoMessage>.Filter.Eq(m => m.Id, message.Id);

            if (_session != null)
                await _collection.ReplaceOneAsync(_session, filter, message);
            else
                await _collection.ReplaceOneAsync(filter, message);
        }

        public async Task DeleteAsync(Guid id)
        {
            var filter = Builders<MongoMessage>.Filter.Eq(m => m.Id, id);

            if (_session != null)
                await _collection.DeleteOneAsync(_session, filter);
            else
                await _collection.DeleteOneAsync(filter);
        }
    }
}