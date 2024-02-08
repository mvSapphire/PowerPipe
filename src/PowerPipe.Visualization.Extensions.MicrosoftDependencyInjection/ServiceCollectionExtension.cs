using System;
using Microsoft.Extensions.Options;
using PowerPipe.Visualization;
using PowerPipe.Visualization.Configurations;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for configuring PowerPipe.Visualization in Microsoft Dependency Injection (DI) services.
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Registers Services required for pipeline visualization
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">The action used to configure the options</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddPowerPipeVisualization(
        this IServiceCollection services, Action<PowerPipeVisualizationConfiguration> configuration)
    {
        var config = new PowerPipeVisualizationConfiguration();
        configuration.Invoke(config);

        services.AddSingleton(_ => Options.Options.Create(config));

        return services.AddSingleton<IPipelineDiagramService, PipelineDiagramsService>();
    }
}
