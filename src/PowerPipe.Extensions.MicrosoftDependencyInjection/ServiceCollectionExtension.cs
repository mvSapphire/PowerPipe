using System;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for configuring PowerPipe in Microsoft Dependency Injection (DI) services.
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Registers Pipeline builder and Step types from the specified assemblies
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">The action used to configure the options</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddPowerPipe(
        this IServiceCollection services, Action<PowerPipeConfiguration> configuration)
    {
        var serviceConfig = new PowerPipeConfiguration();
        configuration.Invoke(serviceConfig);
        return services.AddPowerPipe(serviceConfig);
    }

    /// <summary>
    /// Registers Pipeline builder and Step types from the specified assemblies
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Configuration options</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddPowerPipe(
        this IServiceCollection services, PowerPipeConfiguration configuration)
    {
        if (configuration.AssembliesToRegister.Count == 0)
        {
            throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");
        }

        ServiceRegistrar.AddPowerPipeClasses(services, configuration);
        ServiceRegistrar.AddRequiredServices(services, configuration);
        return services;
    }
}
