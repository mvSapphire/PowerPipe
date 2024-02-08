using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PowerPipe.Visualization;

/// <summary>
/// Middleware for handling visualization requests related to PowerPipe.
/// </summary>
public class PipelineVisualizationMiddleware
{
    private const string IndexHtml = "index.html";
    private const string EndpointPattern = "^/?powerpipe/?$";
    private const string IndexEndpointPattern = $"^/?powerpipe/?{IndexHtml}$";
    private const string DiagramIndexKey = "%DIAGRAMS%";

    private readonly IPipelineDiagramService _pipelineDiagramService;

    private readonly RequestDelegate _next;

    private Func<Stream> IndexStream { get; } = () => typeof(PipelineVisualizationMiddleware).GetTypeInfo().Assembly
        .GetManifestResourceStream("PowerPipe.Visualization.index.html");

    /// <summary>
    /// Initializes a new instance of the <see cref="PipelineVisualizationMiddleware"/> class.
    /// </summary>
    /// <param name="pipelineDiagramService">Service to parse diagrams.</param>
    /// <param name="next">The next middleware in the pipeline.</param>
    public PipelineVisualizationMiddleware(IPipelineDiagramService pipelineDiagramService, RequestDelegate next)
    {
        _pipelineDiagramService = pipelineDiagramService;
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware to handle HTTP requests.
    /// </summary>
    /// <param name="httpContext">The context of the HTTP request.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Invoke(HttpContext httpContext)
    {
        var httpMethod = httpContext.Request.Method;
        var path = httpContext.Request.Path.Value;

        if (httpMethod is WebRequestMethods.Http.Get &&
            Regex.IsMatch(path, EndpointPattern,  RegexOptions.IgnoreCase))
        {
            var relativeIndexUrl = string.IsNullOrEmpty(path) || path.EndsWith('/')
                ? IndexHtml
                : $"{path.Split('/').Last()}/{IndexHtml}";

            RespondWithRedirect(httpContext.Response, relativeIndexUrl);
            return;
        }

        if (httpMethod is WebRequestMethods.Http.Get &&
            Regex.IsMatch(path, IndexEndpointPattern,  RegexOptions.IgnoreCase))
        {
            await RespondWithIndexHtml(httpContext.Response);
            return;
        }

        await _next(httpContext);
    }

    private static void RespondWithRedirect(HttpResponse response, string location)
    {
        response.StatusCode = 301;
        response.Headers["Location"] = location; 
    }

    private async Task RespondWithIndexHtml(HttpResponse response)
    {
        response.StatusCode = 200;
        response.ContentType = MediaTypeNames.Text.Html;

        await using var stream = IndexStream();
        using var reader = new StreamReader(stream);

        var htmlBuilder = new StringBuilder(await reader.ReadToEndAsync());
        htmlBuilder.Replace(DiagramIndexKey, JsonSerializer.Serialize(_pipelineDiagramService.GetDiagrams()));

        await response.WriteAsync(htmlBuilder.ToString(), Encoding.UTF8);
    }
}
