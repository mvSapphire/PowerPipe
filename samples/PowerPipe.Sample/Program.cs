using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using PowerPipe.Sample.Steps;

namespace PowerPipe.Sample;

class Program
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
        services.AddPowerPipe();
        services.AddPowerPipeStep<SampleStep1, SamplePipelineContext>();
        services.AddPowerPipeCompensationStep<SampleStep1Compensation, SamplePipelineContext>();
        services.AddPowerPipeStep<SampleStep2, SamplePipelineContext>();
        services.AddPowerPipeCompensationStep<SampleStep2Compensation, SamplePipelineContext>();
        services.AddPowerPipeStep<SampleStep3, SamplePipelineContext>();
        services.AddPowerPipeStep<SampleStep4, SamplePipelineContext>();
        services.AddPowerPipeStep<SampleStep5, SamplePipelineContext>();
        services.AddPowerPipeStep<SampleStep6, SamplePipelineContext>();
        services.AddPowerPipeStep<SampleStep7, SamplePipelineContext>();
        services.AddPowerPipeCompensationStep<SampleStep7Compensation, SamplePipelineContext>();
        services.AddPowerPipeStep<SampleStep1, SamplePipelineContext>();
        services.AddPowerPipeStep<SampleParallelStep1, SamplePipelineContext>();
        services.AddPowerPipeStep<SampleParallelStep2, SamplePipelineContext>();
        services.AddPowerPipeStep<SampleParallelStep3, SamplePipelineContext>();
        services.AddPowerPipeStep<SampleParallelStep4, SamplePipelineContext>();
        services.AddPowerPipeStep<SampleParallelStep5, SamplePipelineContext>();
        services.AddPowerPipeStep<SampleParallelStep6, SamplePipelineContext>();
        services.AddPowerPipeStep<SampleParallelStep7, SamplePipelineContext>();
        services.AddPowerPipeCompensationStep<SampleParallelStep5Compensation, SamplePipelineContext>();

        services.AddSingleton<SamplePipeline>();

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider;
    }

}
