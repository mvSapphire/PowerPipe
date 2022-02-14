using EasyPipe.Factories;
using EasyPipe.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPipe.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddEasyPipe(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IPipelineStepFactory, PipelineStepFactory>();
    }

    public static IServiceCollection AddEasyPipeStep<TStep>(this IServiceCollection serviceCollection)
        where TStep : class, IPipelineStep
    {
        return serviceCollection.AddTransient<TStep>();
    }
}