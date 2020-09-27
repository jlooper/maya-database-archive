using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MayanGlyphsApi.Models;
using MayanGlyphsApi.Data;
using System.Dynamic;

namespace MayanGlyphsApi
{
    public partial class ArtifactsHttpApi
    {
        private readonly IDocumentStore _documentStore;
        public ArtifactsHttpApi(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        [FunctionName("GetArtifacts")]
        public async Task<IActionResult> GetArtifacts(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "artifacts")] HttpRequest req,
            ILogger log)
        {
            
            var queryParams = new ArtifactResourceQueryParameters();
            queryParams.Fields = req.Query.ContainsKey("fields") ? req.Query["fields"].ToString() : string.Empty;
            queryParams.OrderBy = req.Query.ContainsKey("orderby") ? req.Query["orderby"].ToString() : string.Empty;
            queryParams.Page = req.Query.ContainsKey("page") ? int.Parse(req.Query["page"]) : 1;
            queryParams.Limit = req.Query.ContainsKey("limit") ? int.Parse(req.Query["limit"]) : 10;

            if (!typeof(Artifact).HasProperties(queryParams.Fields))
            {
                return new BadRequestResult();
            }

            var pagedRecords = await _documentStore.GetArtifacts(queryParams);

            var records = pagedRecords.ChooseFields(queryParams.Fields);
            var links = CreateLinksForArtifacts(req.HttpContext, queryParams, pagedRecords.HasNextPage, pagedRecords.HasPreviousPage);
            var responseBody = new ApiResponse<ExpandoObject>(links, records);

            return new OkObjectResult(responseBody);
        }


        [FunctionName("GetArtifactById")]
        public async Task<IActionResult> GetArtifactById(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "artifacts/{id}")] HttpRequest req, string id,
            ILogger log)
        {
            var result = await _documentStore.GetArtifactById(id);

            if (result is null)
                return new NotFoundResult();

            return new OkObjectResult(result);
        }
    }
}
