using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
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
                .ChangeDefaultLifetime(ServiceLifetime.Scoped)
                .AddBehavior(typeof(SampleGenericStep<>), ServiceLifetime.Singleton);
        });

        services.AddSingleton<SamplePipeline>();
        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }

}
