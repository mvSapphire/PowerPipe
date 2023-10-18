using System;
using Microsoft.Extensions.DependencyInjection;
using PowerPipe.Extensions.MicrosoftDependencyInjection;
using PowerPipe.UnitTests.Steps;

namespace PowerPipe.UnitTests.Fixtures;

public sealed class DIFixture : IDisposable
{
    public readonly IServiceCollection ServiceCollection;

    public DIFixture()
    {
        var services = new ServiceCollection();
        services.AddPowerPipe();
        services.AddPowerPipeStep<TestStep1, TestPipelineContext>();
        services.AddPowerPipeStep<TestStep2, TestPipelineContext>();
        services.AddPowerPipeStep<TestParallelStep, TestPipelineContext>();
        services.AddPowerPipeCompensationStep<TestCompensationStep, TestPipelineContext>();

        ServiceCollection = services;
    }

    public void Dispose()
    {
    }
}
