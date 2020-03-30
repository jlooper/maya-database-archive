using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MayanGlyphsApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MayanGlyphsApi.Data
{
    public interface IDocumentStore
    {
        Task<List<Artifact>> GetArtifact(uint page = 0, uint limit = 10);
        Task<Artifact> GetArtifactById(int id);
        IQueryable<Artifact> GetArtifactsQueryable();
        Task<bool> RemoveArtifact(int id);
    }

    public class MongoDocumentStore : IDocumentStore
    {
        private const string COLLECTION_NAME = "glyphs";
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;

        public MongoDocumentStore(IMongoClient mongoClient)
        {
            this._mongoClient = mongoClient;
            this._database = this._mongoClient.GetDatabase("maya-glyph-db");
        }

        public async Task<List<Artifact>> GetArtifact(uint page = 1, uint limit = 10)
        {
            var collection = this._database.GetCollection<Artifact>(COLLECTION_NAME);
            var results = await collection.Find(new BsonDocument())
                            .Skip(Convert.ToInt32((page - 1) * limit)).Limit(Convert.ToInt32(limit))
                            .ToListAsync();
            return results;
        }

        public async Task<Artifact> GetArtifactById(int id)
        {
            var collection = this._database.GetCollection<Artifact>(COLLECTION_NAME);
            var filter = Builders<Artifact>.Filter.Eq(a => a.RecId, id);

            var results = await collection.Find(filter)
                            .FirstOrDefaultAsync();
            return results;
        }

        public IQueryable<Artifact> GetArtifactsQueryable()
        {
            return this._database.GetCollection<Artifact>(COLLECTION_NAME).AsQueryable<Artifact>();
        }

        public async Task<bool> RemoveArtifact(int id)
        {
            var collection = this._database.GetCollection<Artifact>(COLLECTION_NAME);
            var filter = Builders<Artifact>.Filter.Eq(a => a.RecId, id);

            var result = await collection.DeleteOneAsync(filter);
            return result.DeletedCount > 1;

        }
    }
}
