using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PowerPipe.Builder;
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
            .Add<TestStep1>()
            .Build();

        await pipeline.RunAsync(cts.Token);

        context.Step1RunCount.Should().Be(1);
    }
}