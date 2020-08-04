using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace QueryTester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await QueryCosmosSQL();
        }

        private static async Task QueryMongo()
        {
            var cosmosConnection = Environment.GetEnvironmentVariable("COSMOSCONNECTION");
            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets(typeof(Program).Assembly);
            var config = builder.Build();

            var mongoClient = new MongoClient(config["COSMOSCONNECTION"]);
            var database = mongoClient.GetDatabase("maya-db");
            var queryableCollection = database.GetCollection<Artifact>("objects").AsQueryable<Artifact>();

            var query = (from art in queryableCollection
                         where art.Surface != string.Empty
                         orderby art.Surface descending
                         select new { art.Material, art.Surface }).Take(20);

            var count = await query.CountAsync();
            /**
             * Trim is not supported
             */
            var results = await query.ToListAsync();

            foreach (var fact in results)
            {
                Console.WriteLine($"{fact.Surface} - {fact.Material}");
            }
        }
        private static async Task QueryCosmosSQL()
        {
            //CosmosClient cosmos = new CosmosClient("AccountEndpoint=https://maya-glyph-cos-sql.documents.azure.com:443/;AccountKey=QrdVHJxKtjRRWonNvIIZWTOdaPMtmcTa3GfYbHiq22SKelNzqDiRFeduuOv6CtG54IXiftyyWhjcLXeneapBZg==;");
            CosmosClient cosmos = new CosmosClient("https://maya-glyph-cos-sql.documents.azure.com:443/", "QrdVHJxKtjRRWonNvIIZWTOdaPMtmcTa3GfYbHiq22SKelNzqDiRFeduuOv6CtG54IXiftyyWhjcLXeneapBZg==");
            Container container = cosmos.GetContainer("maya-glyph-cos-sql", "objects");
            var queryableContainer = container.GetItemLinqQueryable<Artifact>();


            var query = (from art in queryableContainer
                             // where art.Surface != string.Empty
                         orderby art.Surface descending
                         select new { art.Material, art.Surface }).Take(20);
            try
            {


                var iterator = query.ToFeedIterator();
                while (iterator.HasMoreResults)
                {
                    foreach (var fact in await iterator.ReadNextAsync())
                    {
                        Console.WriteLine($"{fact.Surface} - {fact.Material}");
                    }
                }
            }
            catch (Exception ex)
            {
                await Console.Error.WriteAsync(ex.Message);
            }
        }
    }
}
