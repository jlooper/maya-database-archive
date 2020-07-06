using System.Collections.Generic;

namespace MayanGlyphsApi.Models
{
    public class ApiResponse<T>
    {
        public IEnumerable<ResourceLink> Links { get; private set; }
        public IEnumerable<T> Items { get; private set; }
        public string Version { get; set; } = "1.0";

        public ApiResponse(IEnumerable<ResourceLink> links, IEnumerable<T> items)
        {
            Links = links;
            Items = items;
        }
    }
}