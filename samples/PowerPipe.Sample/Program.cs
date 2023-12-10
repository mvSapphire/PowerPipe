using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PowerPipe.Sample.Steps;

namespace PowerPipe.Sample;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var serviceProvider = ConfigureServices();
        var samplePipeline = serviceProvider.GetService<SamplePipeline>();
        var pipeline = samplePipeline.SetupPipeline();
        await pipeline.RunAsync();
    }

    private static IServiceProvider ConfigureServices()
    {
        //setup dependency injection
        IServiceCollection services = new ServiceCollection();
        
        services.AddPowerPipe(config =>
        {
            config
                .RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())
                .ApplyTypeEvaluation(type => type.Name.StartsWith("Sample")) // not required; in case of few pipelines in one assembly that requires different registrations
                .ChangeStepsDefaultLifetime(ServiceLifetime.Singleton)
                .AddScoped(typeof(SampleGenericStep<>));
        });

        services.AddLogging(options =>
        {
            options
                .AddConsole()
                .SetMinimumLevel(LogLevel.Debug);
        });

        services.AddSingleton<SamplePipeline>();
        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }

}
