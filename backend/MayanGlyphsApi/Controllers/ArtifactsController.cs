using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MayanGlyphsApi.Data;
using MayanGlyphsApi.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]
        public async Task<ActionResult> RetrieveArtifacts(uint page = 1, uint limit = 10)
        {
            page = page < 1 ? 1 : page;
            var records = await _documentStore.GetArtifact(page, limit);
            return Ok(records);
        }        
    }

    [ODataRoutePrefix("artifacts")]
    public class ArtifactsODataController : ODataController
    {
        private readonly IDocumentStore _documentStore;

        public ArtifactsODataController(IDocumentStore documentStore)
        {
            this._documentStore = documentStore;
        }

        [HttpGet]
        [ODataRoute]
        [EnableQuery]
        public ActionResult<IQueryable<Artifact>> RetrieveArtifactsQueryable()
        {
            var records = _documentStore.GetArtifactsQueryable();
            return Ok(records);
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public async Task<ActionResult<Artifact>> GetArtifact([FromODataUri]int id)
        {
            var result = await _documentStore.GetArtifactById(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<ActionResult> DeleteProduct([FromODataUri]int id)
        {
            var result = await _documentStore.RemoveArtifact(id);
     

            return NoContent();
        }
    }
}