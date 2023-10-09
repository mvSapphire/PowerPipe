using System;
using System.Threading;
using System.Threading.Tasks;
using PowerPipe.Interfaces;

namespace PowerPipe.Builder.Steps;

/// <summary>
/// Represents a pipeline step that conditionally executes one of two sub-steps based on a provided predicate.
/// </summary>
/// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
internal class AddIfElseStep<TContext> : InternalStep<TContext>
{
    private readonly IPipelineStep<TContext> _ifStep;
    private readonly IPipelineStep<TContext> _elseStep;
    private readonly Predicate<TContext> _predicate;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddIfElseStep{TContext}"/> class.
    /// </summary>
    /// <param name="predicate">The predicate to determine which sub-step to execute.</param>
    /// <param name="ifStep">The sub-step to execute if the predicate is true.</param>
    /// <param name="elseStep">The sub-step to execute if the predicate is false.</param>
    internal AddIfElseStep(Predicate<TContext> predicate, IPipelineStep<TContext> ifStep, IPipelineStep<TContext> elseStep)
    {
        _predicate = predicate;
        _ifStep = ifStep;
        _elseStep = elseStep;
    }

    /// <summary>
    /// Executes the if-else step asynchronously based on the provided context and predicate.
    /// </summary>
    /// <param name="context">The context on which the if-else step operates.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    protected override async Task ExecuteInternalAsync(TContext context, CancellationToken cancellationToken)
    {
        StepExecuted = true;

        if (_predicate(context))
        {
            _ifStep.NextStep = NextStep;

            await _ifStep.ExecuteAsync(context, cancellationToken);
        }
        else
        {
            _elseStep.NextStep = NextStep;

            await _elseStep.ExecuteAsync(context, cancellationToken);
        }
    }
}