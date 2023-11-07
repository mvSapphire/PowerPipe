using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PowerPipe.Builder;
using PowerPipe.Exceptions;
using PowerPipe.Factories;
using PowerPipe.UnitTests.Fixtures;
using PowerPipe.UnitTests.Steps;
using Xunit;

namespace PowerPipe.UnitTests;

public class PipelineDITests : IClassFixture<DIFixture>
{
    private readonly ServiceProvider _serviceProvider;
    public PipelineDITests(DIFixture diFixture)
    {
        _serviceProvider = diFixture.ServiceCollection.BuildServiceProvider();
    }

    [Fact]
    public async Task TestDI()
    {
        var context = new TestPipelineContext();
        var cts = new CancellationTokenSource();

        var stepFactory = new PipelineStepFactory(_serviceProvider);
        
        var pipeline = new PipelineBuilder<TestPipelineContext, TestPipelineResult>(stepFactory, context)
            .Parallel(b => b
                .Add<TestParallelStep>())
            .Add<TestStep1>()
            .Add<TestGenericStep<TestStep1>>()
            .Add<TestStep2>()
                .CompensateWith<TestCompensationStep>()
            .Build();

        // To check that the compensation step was resolved and executed TestStep2 throws an exception
        var action = () => pipeline.RunAsync(cts.Token);
        await action.Should().ThrowAsync<PipelineExecutionException>();

        context.Step1RunCount.Should().Be(1);
        context.GenericStepRunCount.Should().Be(1);
        context.ParallelStepRunCount.Should().Be(1);
        context.CompensationStepRunCount.Should().Be(1);
    }
}
