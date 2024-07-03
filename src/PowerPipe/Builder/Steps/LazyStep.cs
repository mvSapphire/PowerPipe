using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

/// <summary>
/// Represents a lazy initialization pipeline step.
/// </summary>
/// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
internal class LazyStep<TContext> : InternalStep<TContext>
{
    private readonly Lazy<IStepBase<TContext>> _step;

    /// <summary>
    /// Initializes a new instance of the <see cref="LazyStep{TContext}"/> class.
    /// </summary>
    /// <param name="factory">A factory function to create the step when needed.</param>
    /// <param name="loggerFactory">A logger factory</param>
    internal LazyStep(Func<IStepBase<TContext>> factory, ILoggerFactory loggerFactory)
    {
        _step = new Lazy<IStepBase<TContext>>(() =>
        {
            var instance = factory();

            if (instance is IPipelineStep<TContext> step)
                step.NextStep = NextStep;

            var instanceType = instance.GetType();

            Logger = loggerFactory?.CreateLogger(instanceType);

            Logger?.LogDebug("{Step} executing", instanceType.FullName);

            return instance;
        });
    }

    /// <summary>
    /// Executes the lazy initialization pipeline step asynchronously.
    /// </summary>
    /// <param name="context">The context on which the step operates.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override async ValueTask ExecuteInternalAsync(TContext context, CancellationToken cancellationToken)
    {
        StepExecuted = true;

        await _step.Value.ExecuteAsync(context, cancellationToken);
    }
}
