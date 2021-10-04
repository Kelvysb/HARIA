using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HARIA.Domain.Abstractions.Data;
using HARIA.Domain.Entities;
using HARIA.Domain.Helpers;
using HARIA.Domain.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HARIA.Data
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : EntityBase, new()
    {
        protected IMongoCollection<TEntity> collection;

        public RepositoryBase(MongoDbConfig mongoDbConfig)
        {
            var mongoClient = new MongoClient(mongoDbConfig.ConnectionString);
            CreateCollection(mongoClient, mongoDbConfig);
            collection = mongoClient.GetDatabase(mongoDbConfig.DatabaseName).GetCollection<TEntity>(AttributeHelper.GetCollectionName<TEntity>());
            CreateIndexes();
        }

        public virtual async Task BulkUpsert(List<TEntity> entities)
        {
            entities = entities.Select(e =>
            {
                if (string.IsNullOrEmpty(e.Id))
                    e.Id = ObjectId.GenerateNewId().ToString();
                return e;
            })
            .ToList();

            var bulkOps = entities
               .Select(entity => new ReplaceOneModel<TEntity>(Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id), entity) { IsUpsert = true });
            await collection.BulkWriteAsync(bulkOps, new BulkWriteOptions { BypassDocumentValidation = true, IsOrdered = false });
        }

        public virtual async Task Delete(string id)
        {
            await collection.DeleteOneAsync(e => e.Id == id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            var query = await collection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetById(string id)
        {
            var query = await collection.FindAsync(Builders<TEntity>.Filter.Eq(e => e.Id, id));
            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task Upsert(TEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
                entity.Id = ObjectId.GenerateNewId().ToString();
            await collection.ReplaceOneAsync(e => e.Id == entity.Id, entity, new ReplaceOptions { IsUpsert = true });
        }

        protected abstract Task CreateIndexes();

        private void CreateCollection(MongoClient mongoClient, MongoDbConfig mongoDbConfig)
        {
            var filter = new BsonDocument("name", AttributeHelper.GetCollectionName<TEntity>());
            var options = new ListCollectionNamesOptions { Filter = filter };
            var database = mongoClient.GetDatabase(mongoDbConfig.DatabaseName);

            if (!database.ListCollectionNames(options).Any())
            {
                database.CreateCollection(AttributeHelper.GetCollectionName<TEntity>());
            }
        }
    }
}