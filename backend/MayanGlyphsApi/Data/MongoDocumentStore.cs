using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;
using System.Threading.Tasks;
using MayanGlyphsApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MayanGlyphsApi.Data
{
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

        public async Task<List<Artifact>> GetArtifacts(int page = 1, int limit = 10)
        {
            var collection = this._database.GetCollection<Artifact>(COLLECTION_NAME);
            var results = await collection.Find(new BsonDocument())
                            .Skip((page - 1) * limit).Limit(Convert.ToInt32(limit))
                            .ToListAsync();
            return results;
        }


        public async Task<IPagedList<Artifact>> GetArtifacts(ResourceQueryParameters resourceQuery)
        {
            IQueryable<Artifact> queryable = this._database.GetCollection<Artifact>(COLLECTION_NAME).AsQueryable();
            queryable = queryable.ApplySort(resourceQuery.OrderBy);

            IPagedList<Artifact> pagedResults = await queryable.ToPagedListAsync(resourceQuery.Page, resourceQuery.Limit);

            return pagedResults;
        }

        public async Task<Artifact> GetArtifactById(string id)
        {
            var collection = this._database.GetCollection<Artifact>(COLLECTION_NAME);
            var filter = Builders<Artifact>.Filter.Eq(a => a.Id, id);

            var results = await collection.Find(filter)
                            .FirstOrDefaultAsync();
            return results;
        }

        public IQueryable<Artifact> GetArtifactsQueryable()
        {
            return this._database.GetCollection<Artifact>(COLLECTION_NAME).AsQueryable<Artifact>();
        }

        public async Task<bool> RemoveArtifact(string id)
        {
            var collection = this._database.GetCollection<Artifact>(COLLECTION_NAME);
            var filter = Builders<Artifact>.Filter.Eq(a => a.Id, id);

            var result = await collection.DeleteOneAsync(filter);
            return result.DeletedCount > 1;

        }
    }
}
