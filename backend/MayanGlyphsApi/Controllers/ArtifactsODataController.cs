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
            try
            {
                var records = _documentStore.GetArtifactsQueryable();
                return Ok(records);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [EnableQuery]
        [ODataRoute("({id})")]
        public async Task<ActionResult<Artifact>> GetArtifact([FromODataUri] string id)
        {
            var result = await _documentStore.GetArtifactById(id);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete]
        [ODataRoute("({id})")]
        public async Task<ActionResult> DeleteProduct([FromODataUri] string id)
        {
            var result = await _documentStore.RemoveArtifact(id);

            return NoContent();
        }
    }
}
