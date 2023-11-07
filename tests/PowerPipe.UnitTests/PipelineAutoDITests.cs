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

public class PipelineAutoDITests : IClassFixture<AutoDIFixture>
{
    private readonly ServiceProvider _serviceProvider;
    public PipelineAutoDITests(AutoDIFixture diFixture)
    {
        _serviceProvider = diFixture.ServiceCollection.BuildServiceProvider();
    }

    [Fact]
    public async Task TestAutoDI()
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
    
    [Fact]
    public void TestAutoDIRegistration()
    {
        var services = new ServiceCollection();
        services.AddPowerPipe(c =>
        {
            c.RegisterServicesFromAssemblies(typeof(AutoDIFixture).Assembly)
                .AddBehavior<TestStep1>(ServiceLifetime.Singleton)
                .AddBehavior<TestStep2>(ServiceLifetime.Scoped)
                // default Transient
                .AddBehavior<TestStep3>();
        });
        
        var serviceProvider = services.BuildServiceProvider();
        var test1_1 = serviceProvider.GetService(typeof(TestStep1));
        test1_1.Should().NotBeNull();
        TestStep1.CreationCount.Should().Be(1);
        var test1_2 = serviceProvider.GetService(typeof(TestStep1));
        test1_2.Should().NotBeNull();
        TestStep1.CreationCount.Should().Be(1);
        using var scope_1= serviceProvider.CreateScope();
        var test1_3 = scope_1.ServiceProvider.GetService(typeof(TestStep1));
        test1_3.Should().NotBeNull();
        TestStep1.CreationCount.Should().Be(1);
        
        var test2_1 = serviceProvider.GetService<TestStep2>();
        test2_1.Should().NotBeNull();
        TestStep2.CreationCount.Should().Be(1);
        using var scope_2 = serviceProvider.CreateScope();
        var test2_2 = scope_2.ServiceProvider.GetService<TestStep2>();
        test2_2.Should().NotBeNull();
        TestStep2.CreationCount.Should().Be(2);
        var test2_3 = scope_2.ServiceProvider.GetService<TestStep2>();
        test2_3.Should().NotBeNull();
        TestStep2.CreationCount.Should().Be(2);
        
        var test3_1 = serviceProvider.GetService<TestStep3>();
        test3_1.Should().NotBeNull();
        TestStep3.CreationCount.Should().Be(1);
        using var scope_3 = serviceProvider.CreateScope();
        var test3_2 = scope_3.ServiceProvider.GetService<TestStep3>();
        test3_2.Should().NotBeNull();
        TestStep2.CreationCount.Should().Be(2);
        var test3_3 = scope_3.ServiceProvider.GetService<TestStep3>();
        test3_3.Should().NotBeNull();
        TestStep3.CreationCount.Should().Be(3);
    }
}
