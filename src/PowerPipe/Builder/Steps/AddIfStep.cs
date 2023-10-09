using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

/// <summary>
/// Represents a pipeline step that conditionally executes a sub-step based on a provided predicate.
/// </summary>
/// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
internal class AddIfStep<TContext> : InternalStep<TContext>
{
    private readonly IPipelineStep<TContext> _step;
    private readonly Predicate<TContext> _predicate;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddIfStep{TContext}"/> class.
    /// </summary>
    /// <param name="predicate">The predicate to determine whether to execute the sub-step.</param>
    /// <param name="step">The sub-step to execute if the predicate is true.</param>
    internal AddIfStep(Predicate<TContext> predicate, IPipelineStep<TContext> step)
    {
        _predicate = predicate;
        _step = step;
    }

    /// <summary>
    /// Executes the if step asynchronously based on the provided context and predicate.
    /// </summary>
    /// <param name="context">The context on which the if step operates.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override async Task ExecuteInternalAsync(TContext context, CancellationToken cancellationToken)
    {
        if (_predicate(context))
        {
            StepExecuted = true;

            _step.NextStep = NextStep;

            await _step.ExecuteAsync(context, cancellationToken);
        }
        else
        {
            if (NextStep is not null)
                await NextStep.ExecuteAsync(context, cancellationToken);
        }
    }
}