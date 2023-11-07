using System;
using PowerPipe.Factories;
using PowerPipe.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for configuring PowerPipe in Microsoft Dependency Injection (DI) services.
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// Adds PowerPipe services to the DI container with the specified lifetime.
    /// </summary>
    /// <param name="serviceCollection">The DI service collection to which services are added.</param>
    /// <param name="lifetime">The lifetime of the added services (Transient, Scoped, or Singleton).</param>
    /// <returns>The modified DI service collection.</returns>
    [Obsolete("Use new registration with PowerPipeConfiguration")]
    public static IServiceCollection AddPowerPipe(
        this IServiceCollection serviceCollection, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        return lifetime switch
        {
            ServiceLifetime.Transient => serviceCollection.AddTransient<IPipelineStepFactory, PipelineStepFactory>(),
            ServiceLifetime.Scoped => serviceCollection.AddScoped<IPipelineStepFactory, PipelineStepFactory>(),
            ServiceLifetime.Singleton => serviceCollection.AddSingleton<IPipelineStepFactory, PipelineStepFactory>(),
            _ => throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null)
        };
    }

    /// <summary>
    /// Adds a PowerPipe step to the DI container with the specified lifetime.
    /// </summary>
    /// <typeparam name="TStep">The type of the step to add.</typeparam>
    /// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
    /// <param name="serviceCollection">The DI service collection to which the step is added.</param>
    /// <param name="lifetime">The lifetime of the added step (Transient, Scoped, or Singleton).</param>
    /// <returns>The modified DI service collection.</returns>
    [Obsolete("Use new registration with PowerPipeConfiguration")]
    public static IServiceCollection AddPowerPipeStep<TStep, TContext>(
        this IServiceCollection serviceCollection, ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TStep : class, IStepBase<TContext>
        where TContext : class
    {
        return lifetime switch
        {
            ServiceLifetime.Transient => serviceCollection.AddTransient<TStep>(),
            ServiceLifetime.Scoped => serviceCollection.AddScoped<TStep>(),
            ServiceLifetime.Singleton => serviceCollection.AddSingleton<TStep>(),
            _ => throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null)
        };
    }

    /// <summary>
    /// Adds a PowerPipe compensation step to the DI container with the specified lifetime.
    /// </summary>
    /// <typeparam name="TStep">The type of the compensation step to add.</typeparam>
    /// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
    /// <param name="serviceCollection">The DI service collection to which the compensation step is added.</param>
    /// <param name="lifetime">The lifetime of the added compensation step (Transient, Scoped, or Singleton).</param>
    /// <returns>The modified DI service collection.</returns>
    [Obsolete("Use new registration with PowerPipeConfiguration")]
    public static IServiceCollection AddPowerPipeCompensationStep<TStep, TContext>(
        this IServiceCollection serviceCollection, ServiceLifetime lifetime = ServiceLifetime.Transient)
        where TStep : class, IPipelineCompensationStep<TContext>
        where TContext : class
    {
        return lifetime switch
        {
            ServiceLifetime.Transient => serviceCollection.AddTransient<TStep>(),
            ServiceLifetime.Scoped => serviceCollection.AddScoped<TStep>(),
            ServiceLifetime.Singleton => serviceCollection.AddSingleton<TStep>(),
            _ => throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null)
        };
    }
    
    /// <summary>
    /// Registers Pipeline builder and Step types from the specified assemblies
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">The action used to configure the options</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddPowerPipe(this IServiceCollection services, 
        Action<PowerPipeConfiguration> configuration)
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
    public static IServiceCollection AddPowerPipe(this IServiceCollection services, 
        PowerPipeConfiguration configuration)
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
