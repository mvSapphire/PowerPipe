using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PowerPipe.Builder;
using PowerPipe.Factories;
using PowerPipe.Interfaces;

namespace PowerPipe.UnitTests;

public class PipelineTests
{
    [Fact]
    public async Task AddStep_Succeed()
    {
        var step = Substitute.For<TestStep1>();
        var context = new TestPipelineContext();
        var cts = new CancellationTokenSource();

        var stepFactory =
            new PipelineStepFactory(new ServiceCollection()
                .AddTransient(_ => step)
                .BuildServiceProvider());

        var pipeline = new PipelineBuilder<TestPipelineContext, TestPipelineResult>(stepFactory, context)
            .Add<TestStep1>()
            .Build();

        await pipeline.RunAsync(cts.Token);

        await step.Received().ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task AddIfStep_Succeed(bool predicate)
    {
        var step = Substitute.For<TestStep1>();
        var context = new TestPipelineContext();
        var cts = new CancellationTokenSource();

        var stepFactory =
            new PipelineStepFactory(new ServiceCollection()
                .AddTransient(_ => step)
                .BuildServiceProvider());

        var pipeline = new PipelineBuilder<TestPipelineContext, TestPipelineResult>(stepFactory, context)
            .AddIf<TestStep1>(_ => predicate)
            .Build();

        await pipeline.RunAsync(cts.Token);

        var task = predicate
            ? step.Received().ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token))
            : step.DidNotReceive().ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token));

        await task;
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task AddIfElseStep_Succeed(bool predicate)
    {
        var step1 = Substitute.For<TestStep1>();
        var step2 = Substitute.For<TestStep2>();
        var context = new TestPipelineContext();
        var cts = new CancellationTokenSource();

        var stepFactory =
            new PipelineStepFactory(new ServiceCollection()
                .AddTransient(_ => step1)
                .AddTransient(_ => step2)
                .BuildServiceProvider());

        var pipeline = new PipelineBuilder<TestPipelineContext, TestPipelineResult>(stepFactory, context)
            .AddIfElse<TestStep1, TestStep2>(_ => predicate)
            .Build();

        await pipeline.RunAsync(cts.Token);

        if (predicate)
        {
            await step1.Received().ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token));
            await step2.DidNotReceive().ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token));
        }
        else
        {
            await step2.Received().ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token));
            await step1.DidNotReceive().ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token));
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task IfStep_Succeed(bool predicate)
    {
        var step = Substitute.For<TestStep1>();
        var context = new TestPipelineContext();
        var cts = new CancellationTokenSource();

        var stepFactory =
            new PipelineStepFactory(new ServiceCollection()
                .AddTransient(_ => step)
                .BuildServiceProvider());

        var pipeline = new PipelineBuilder<TestPipelineContext, TestPipelineResult>(stepFactory, context)
            .If(_ => predicate, b => b
                .Add<TestStep1>())
            .Build();

        await pipeline.RunAsync(cts.Token);

        var task = predicate
            ? step.Received().ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token))
            : step.DidNotReceive().ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token));

        await task;
    }
}

public record TestPipelineResult;

public class TestPipelineContext : PipelineContext<TestPipelineResult>
{
    public override TestPipelineResult GetPipelineResult() => new();
}

public class TestStep1 : IPipelineStep<TestPipelineContext>
{
    public IPipelineStep<TestPipelineContext> NextStep { get; set; }

    public Task ExecuteAsync(TestPipelineContext context, CancellationToken cancellationToken) =>
        Task.CompletedTask;
}

public class TestStep2 : IPipelineStep<TestPipelineContext>
{
    public IPipelineStep<TestPipelineContext> NextStep { get; set; }

    public Task ExecuteAsync(TestPipelineContext context, CancellationToken cancellationToken) =>
        Task.CompletedTask;
}