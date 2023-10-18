using System;
using System.Linq;
using PowerPipe.Factories;
using PowerPipe.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace PowerPipe.Extensions.MicrosoftDependencyInjection;

public static class ServiceCollectionExtension
{
    [Obsolete("Use new resgistration with PowerPipeConfiguration")]
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

    [Obsolete("Use new resgistration with PowerPipeConfiguration")]
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

    [Obsolete("Use new resgistration with PowerPipeConfiguration")]
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
        if (!configuration.AssembliesToRegister.Any())
        {
            throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");
        }

        ServiceRegistrar.AddPowerPipeClasses(services, configuration);
        ServiceRegistrar.AddRequiredServices(services, configuration);
        return services;
    }
}