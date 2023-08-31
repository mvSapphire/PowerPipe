using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using PowerPipe.Builder;
using PowerPipe.Builder.Steps;
using PowerPipe.Extensions.MicrosoftDependencyInjection;
using PowerPipe.Factories;
using PowerPipe.UnitTests.Steps;

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

        await step.Received(1).ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token));
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
            ? step.Received(1).ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token))
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
            await step1.Received(1).ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token));
            await step2.DidNotReceive().ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token));
        }
        else
        {
            await step2.Received(1).ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token));
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
            ? step.Received(1).ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token))
            : step.DidNotReceive().ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token));

        await task;
    }

    [Theory]
    [InlineData(PipelineStepErrorHandling.Suppress, true)]
    [InlineData(PipelineStepErrorHandling.Retry, true)]
    [InlineData(PipelineStepErrorHandling.Suppress, false)]
    [InlineData(PipelineStepErrorHandling.Retry, false)]
    public async Task OnError_Succeed(PipelineStepErrorHandling errorHandlingBehaviour, bool applyErrorHandling)
    {
        var step = Substitute.For<TestStep1>();

        var exceptionMessage = "Test message";

        step.ExecuteAsync(Arg.Any<TestPipelineContext>(), Arg.Any<CancellationToken>())
            .Returns(_ => throw new InvalidOperationException(exceptionMessage));

        var context = new TestPipelineContext();
        var cts = new CancellationTokenSource();

        var stepFactory =
            new PipelineStepFactory(new ServiceCollection()
                .AddTransient(_ => step)
                .BuildServiceProvider());

        var retryCount = 3;
        var isRetryBehaviour = errorHandlingBehaviour is PipelineStepErrorHandling.Retry;

        bool ShouldApplyErrorHandling(TestPipelineContext _) => applyErrorHandling;

        var pipeline = new PipelineBuilder<TestPipelineContext, TestPipelineResult>(stepFactory, context)
            .Add<TestStep1>()
                .OnError(errorHandlingBehaviour, maxRetryCount: isRetryBehaviour ? retryCount : default, predicate: ShouldApplyErrorHandling)
            .Build();

        var action = () => pipeline.RunAsync(cts.Token);

        if (applyErrorHandling)
        {
            if (isRetryBehaviour)
            {
                await action.Should().ThrowAsync<InvalidOperationException>().WithMessage(exceptionMessage);

                await step.Received(1 + retryCount).ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token));
            }
            else
            {
                await action.Should().NotThrowAsync<InvalidOperationException>();

                await step.Received(1).ExecuteAsync(Arg.Is(context), Arg.Is(cts.Token));
            }
        }
        else
        {
            await action.Should().ThrowAsync<InvalidOperationException>().WithMessage(exceptionMessage);
        }
    }

    [Fact]
    public async Task CompensateWith_Succeed()
    {
        var step2 = Substitute.For<TestStep2>();
        var compensationStep = Substitute.For<TestCompensationStep>();

        var exceptionMessage = "Test message";

        step2.ExecuteAsync(Arg.Any<TestPipelineContext>(), Arg.Any<CancellationToken>())
            .Returns(_ => throw new InvalidOperationException(exceptionMessage));

        var context = new TestPipelineContext();
        var cts = new CancellationTokenSource();

        var stepFactory =
            new PipelineStepFactory(new ServiceCollection()
                .AddPowerPipeStep<TestStep1, TestPipelineContext>()
                .AddPowerPipeStep<TestStep2, TestPipelineContext>()
                .AddTransient(_ => compensationStep)
                .BuildServiceProvider());

        var pipeline = new PipelineBuilder<TestPipelineContext, TestPipelineResult>(stepFactory, context)
            .Add<TestStep1>()
                .CompensateWith<TestCompensationStep>()
            .Add<TestStep2>()
                .CompensateWith<TestCompensationStep>()
            .Build();

        var action = () => pipeline.RunAsync(cts.Token);

        await action.Should().ThrowAsync<Exception>();

        await compensationStep.Received(2).CompensateAsync(Arg.Is(context), Arg.Is(cts.Token));
    }
}
