using MayanGlyphsApi.Data;
using MayanGlyphsApi.Data.Mongo;
using MayanGlyphsApi.Models;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

[assembly: FunctionsStartup(typeof(MayanGlyphsApi.Startup))]
namespace MayanGlyphsApi
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            BsonClassMap.RegisterClassMap<Artifact>(map =>
           {
               map.AutoMap();
               map.SetIgnoreExtraElements(true);
               map.MapMember(a => a.Coordinate)
                   .SetSerializer(new StringOrInt32Serializer());
               map.MapMember(a => a.Cycle260)
                   .SetSerializer(new StringOrInt32Serializer());
               map.MapMember(a => a.Cycl365)
                   .SetSerializer(new StringOrInt32Serializer());
               map.MapMember(a => a.BlEng)
                   .SetSerializer(new StringOrInt32Serializer());
               map.MapMember(a => a.BlSpan)
                 .SetSerializer(new StringOrInt32Serializer());
               map.MapMember(a => a.Hyphenated)
                 .SetSerializer(new StringOrInt32Serializer());
               map.MapMember(a => a.GraphCodes)
                 .SetSerializer(new StringOrInt32Serializer());
               map.MapMember(a => a.Blocklogosyllabic)
                 .SetSerializer(new StringOrInt32Serializer());
           });
            
            builder.Services.AddSingleton<IMongoClient>(provider =>
                       {
                           var config = provider.GetService<IConfiguration>();
                           return new MongoClient(config["COSMOSCONNECTION"]);
                       });
            builder.Services.AddSingleton<IDocumentStore, MongoDocumentStore>();
        }
    }
}