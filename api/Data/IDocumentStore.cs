using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MayanGlyphsApi.Models;
using X.PagedList;

namespace MayanGlyphsApi.Data
{
    public interface IDocumentStore
    {
        Task<IPagedList<Artifact>> GetArtifacts(ResourceQueryParameters resourceQuery);
        Task<List<Artifact>> GetArtifacts(int page = 0, int limit = 10);
        Task<Artifact> GetArtifactById(string id);
        IQueryable<Artifact> GetArtifactsQueryable();
        Task<bool> RemoveArtifact(string id);
    }
}