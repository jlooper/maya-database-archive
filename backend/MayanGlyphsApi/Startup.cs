using MayanGlyphsApi.Data;
using MayanGlyphsApi.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using MongoDB.Driver;

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
            services.AddControllers();
            services.AddOData();            
            services.AddSingleton<IMongoClient>(provider =>
            {
                var config = provider.GetService<IConfiguration>();
                return new MongoClient(config["COSMOSCONNECTION"]);
            });
            services.AddSingleton<IDocumentStore, MongoDocumentStore>();
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
                //endpoints.EnableDependencyInjection();
                endpoints.Select().Filter().OrderBy().Count().MaxTop(30);
                endpoints.MapODataRoute("odataroute", "odata", GetEdmModel());
            });
        }

        private IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            var artifactEntitySet = odataBuilder.EntitySet<Artifact>("Artifacts");
            artifactEntitySet.EntityType.HasKey(e => e.RecId);

            return odataBuilder.GetEdmModel();
        }

    }
}
