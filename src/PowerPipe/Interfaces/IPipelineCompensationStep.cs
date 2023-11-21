using System.Threading;
using System.Threading.Tasks;

namespace PowerPipe.Interfaces;

/// <summary>
/// Represents a compensation step to be executed in case of a pipeline failure.
/// </summary>
/// <typeparam name="TContext">The type of context used in the pipeline.</typeparam>
public interface IPipelineCompensationStep<in TContext>
{
    /// <summary>
    /// Compensates for a failed pipeline execution asynchronously.
    /// </summary>
    /// <param name="context">The context on which the compensation step operates.</param>
    /// <returns>A task representing the asynchronous compensation operation.</returns>
    ValueTask CompensateAsync(TContext context) => CompensateAsync(context, default);

    /// <summary>
    /// Compensates for a failed pipeline execution asynchronously with a cancellation token.
    /// </summary>
    /// <param name="context">The context on which the compensation step operates.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the compensation operation.</param>
    /// <returns>A task representing the asynchronous compensation operation.</returns>
    ValueTask CompensateAsync(TContext context, CancellationToken cancellationToken);
}
