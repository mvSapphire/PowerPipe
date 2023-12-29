using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PowerPipe.Visualization.Core;

public class VisualizationMiddleware
{
    private readonly RequestDelegate _next;

    private Func<Stream> IndexStream { get; } = () => typeof(VisualizationMiddleware).GetTypeInfo().Assembly
        .GetManifestResourceStream("PowerPipe.Visualization.Core.index.html");

    public VisualizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        var httpMethod = httpContext.Request.Method;
        var path = httpContext.Request.Path.Value;

        if (httpMethod == "GET" && Regex.IsMatch(path, $"^/?{Regex.Escape("powerpipe")}/?$",  RegexOptions.IgnoreCase))
        {
            var relativeIndexUrl = string.IsNullOrEmpty(path) || path.EndsWith('/')
                ? "index.html"
                : $"{path.Split('/').Last()}/index.html";

            RespondWithRedirect(httpContext.Response, relativeIndexUrl);
            return;
        }

        if (httpMethod == "GET" && Regex.IsMatch(path, $"^/{Regex.Escape("powerpipe")}/?index.html$",  RegexOptions.IgnoreCase))
        {
            await RespondWithIndexHtml(httpContext.Response);
            return;
        }

        await _next(httpContext);
    }

    private void RespondWithRedirect(HttpResponse response, string location)
    {
        response.StatusCode = 301;
        response.Headers["Location"] = location;
    }

    private async Task RespondWithIndexHtml(HttpResponse response)
    {
        response.StatusCode = 200;
        response.ContentType = "text/html;charset=utf-8";

        await using var stream = IndexStream();
        using var reader = new StreamReader(stream);

        // Inject arguments before writing to response
        var htmlBuilder = new StringBuilder(await reader.ReadToEndAsync());
        // foreach (var entry in GetIndexArguments())
        // {
        //     htmlBuilder.Replace(entry.Key, entry.Value);
        // }

        await response.WriteAsync(htmlBuilder.ToString(), Encoding.UTF8);
    }
}
