using System.Threading;
using System.Threading.Tasks;

namespace PowerPipe.Interfaces;

/// <summary>
/// Represents the base interface for a step in a pipeline.
/// </summary>
/// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
public interface IStepBase<in TContext>
{
    /// <summary>
    /// Executes the step asynchronously.
    /// </summary>
    /// <param name="context">The context on which the step operates.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    ValueTask ExecuteAsync(TContext context) => ExecuteAsync(context, default);

    /// <summary>
    /// Executes the step asynchronously with a cancellation token.
    /// </summary>
    /// <param name="context">The context on which the step operates.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    ValueTask ExecuteAsync(TContext context, CancellationToken cancellationToken);
}
