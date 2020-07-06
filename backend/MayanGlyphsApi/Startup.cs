using System.Reflection;
using MayanGlyphsApi.Data;
using MayanGlyphsApi.Data.Mongo;
using MayanGlyphsApi.Models;
using MediatR;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MayanGlyphsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

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

            services.AddSingleton<IMongoClient>(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                return new MongoClient(config["COSMOSCONNECTION"]);
            });

            services.AddOData();

            services.AddSingleton<IDocumentStore, MongoDocumentStore>();

            services.AddControllers().AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                opts.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                opts.SerializerSettings.Formatting = Formatting.None;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapODataRoute("odataroute", "odata", GetEdmModel())
                  .Select().Filter().OrderBy().Count().MaxTop(30);
            });
        }

        private IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            var artifactEntitySet = odataBuilder.EntitySet<Artifact>("Artifacts");
            artifactEntitySet.EntityType.HasKey(e => e.Id);

            return odataBuilder.GetEdmModel();
        }
    }
}
