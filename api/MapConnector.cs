using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace api
{
    public class MapConnector
    {
        IConfiguration configuration;
        public MapConnector(IConfiguration configuration)
        {

            this.configuration = configuration;

        }
        [FunctionName("mapsettings")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var key = configuration["VUE_APP_MAP_KEY"];
            return new OkObjectResult(key);
        }
    }
}
