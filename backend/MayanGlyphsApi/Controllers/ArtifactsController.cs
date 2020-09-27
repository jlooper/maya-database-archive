using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using MayanGlyphsApi.Data;
using MayanGlyphsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace MayanGlyphsApi.Controllers
{
    [Route("api/artifacts")]
    [ApiController]
    public class ArtifactsController : ControllerBase
    {
        private readonly IDocumentStore _documentStore;

        public ArtifactsController(IDocumentStore documentStore)
        {
            this._documentStore = documentStore;
        }

        [HttpGet(Name = "GetArtifacts")]
        public async Task<ActionResult> RetrieveArtifacts([FromQuery] ArtifactResourceQueryParameters queryParams)
        {
            if (!typeof(Artifact).HasProperties(queryParams.Fields))
            {
                return BadRequest();
            }
            var pagedRecords = await _documentStore.GetArtifacts(queryParams);

            var records = pagedRecords.ChooseFields(queryParams.Fields);
            var links = CreateLinksForArtifacts(queryParams, pagedRecords.HasNextPage, pagedRecords.HasPreviousPage);
            var responseBody = new ApiResponse<ExpandoObject>(links, records);

            return Ok(responseBody);
        }

        [HttpGet("{id}", Name = "GetArtifactById")]
        public async Task<ActionResult<Artifact>> GetArtifact(string id)
        {
            var result = await _documentStore.GetArtifactById(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        private string CreateArtifactsResourceUri(ArtifactResourceQueryParameters artifactParameters, ResourceUriType type)
        {
            return type switch
            {
                ResourceUriType.PreviousPage => Url.Link("GetArtifacts",
                     new
                     {
                         fields = artifactParameters.Fields,
                         orderBy = artifactParameters.OrderBy,
                         page = artifactParameters.Page - 1,
                         limit = artifactParameters.Limit
                     }),
                ResourceUriType.NextPage => Url.Link("GetArtifacts",
                     new
                     {
                         fields = artifactParameters.Fields,
                         orderBy = artifactParameters.OrderBy,
                         page = artifactParameters.Page + 1,
                         limit = artifactParameters.Limit
                     }),
                ResourceUriType.Current => Url.Link("GetArtifacts",
                  new
                  {
                      fields = artifactParameters.Fields,
                      orderBy = artifactParameters.OrderBy,
                      page = artifactParameters.Page,
                      limit = artifactParameters.Limit
                  }),
                _ => string.Empty
            };
        }

        private IEnumerable<ResourceLink> CreateLinksForArtifacts(ArtifactResourceQueryParameters artifactParameters,
           bool hasNext, bool hasPrevious)
        {
            var links = new List<ResourceLink>
            {
                // self links
                new ResourceLink(
                   CreateArtifactsResourceUri(artifactParameters, ResourceUriType.Current),
                    "self", "GET")
            };

            if (hasNext)
            {
                links.Add(
                  new ResourceLink(
                      CreateArtifactsResourceUri(artifactParameters, ResourceUriType.NextPage),
                     "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new ResourceLink(
                        CreateArtifactsResourceUri(artifactParameters, ResourceUriType.PreviousPage),
                        "previousPage", "GET"));
            }

            return links;
        }
    }
}