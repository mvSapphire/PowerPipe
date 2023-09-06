using System;
using PowerPipe.Factories;
using PowerPipe.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace PowerPipe.Extensions.MicrosoftDependencyInjection;

public static class ServiceCollectionExtension
{
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
}