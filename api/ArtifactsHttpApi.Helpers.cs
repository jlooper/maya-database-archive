using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Web;
using MayanGlyphsApi.Models;
using Microsoft.AspNetCore.Http;

namespace MayanGlyphsApi
{
    public partial class ArtifactsHttpApi
    {
        private string CreateArtifactsResourceUri(HttpContext context, ArtifactResourceQueryParameters artifactParameters, ResourceUriType type)
        {
            var uriBuilder = new UriBuilder(context.Request.Scheme, context.Request.Host.Host)
            {
                Path = $"api/artifacts",
            };

            if (context.Request.Host.Port.HasValue)
            {
                uriBuilder.Port = context.Request.Host.Port.Value;
            }

            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["fields"] = artifactParameters.Fields;
            query["orderBy"] = artifactParameters.OrderBy;
            query["limit"] = artifactParameters.Limit.ToString();

            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    query["page"] = (artifactParameters.Page - 1).ToString();
                    uriBuilder.Query = query.ToString();
                    return uriBuilder.ToString();

                case ResourceUriType.NextPage:
                    query["page"] = (artifactParameters.Page + 1).ToString();
                    uriBuilder.Query = query.ToString();
                    return uriBuilder.ToString();

                case ResourceUriType.Current:
                    query["page"] = (artifactParameters.Page).ToString();
                    uriBuilder.Query = query.ToString();
                    return uriBuilder.ToString();
                default:
                    return string.Empty;
            }
        }

        private IEnumerable<ResourceLink> CreateLinksForArtifacts(HttpContext context,
           ArtifactResourceQueryParameters artifactParameters,
           bool hasNext, bool hasPrevious)
        {
            var links = new List<ResourceLink>
            {
                // self links
                new ResourceLink(
                   CreateArtifactsResourceUri(context,artifactParameters, ResourceUriType.Current),
                    "self", "GET")
            };

            if (hasNext)
            {
                links.Add(
                  new ResourceLink(
                      CreateArtifactsResourceUri(context, artifactParameters, ResourceUriType.NextPage),
                     "nextPage", "GET"));
            }

            if (hasPrevious)
            {
                links.Add(
                    new ResourceLink(
                        CreateArtifactsResourceUri(context, artifactParameters, ResourceUriType.PreviousPage),
                        "previousPage", "GET"));
            }

            return links;
        }

    }
}