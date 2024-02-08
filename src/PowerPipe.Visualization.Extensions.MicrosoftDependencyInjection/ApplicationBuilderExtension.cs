using Microsoft.AspNetCore.Builder;
using PowerPipe.Visualization;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension methods for configuring PowerPipe visualization middleware.
/// </summary>
public static class ApplicationBuilderExtension
{
    /// <summary>
    /// Adds PowerPipe visualization middleware to the application's request pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance for method chaining.</returns>
    public static IApplicationBuilder UsePowerPipeVisualization(this IApplicationBuilder app) =>
        app.UseMiddleware<PipelineVisualizationMiddleware>();
}
