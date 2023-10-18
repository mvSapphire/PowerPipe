using System.Threading;
using System.Threading.Tasks;

namespace PowerPipe.Interfaces;

/// <summary>
/// Represents a pipeline for executing a series of steps.
/// </summary>
/// <typeparam name="TResult">The type of result returned by the pipeline.</typeparam>
public interface IPipeline<TResult>
{
    /// <summary>
    /// Runs the pipeline asynchronously with the option to return a result.
    /// </summary>
    /// <param name="returnResult">A flag indicating whether to return a result.</param>
    /// <returns>A task representing the asynchronous operation and optionally the result.</returns>
    Task<TResult> RunAsync(bool returnResult = true) => RunAsync(default, returnResult);

    /// <summary>
    /// Runs the pipeline asynchronously with the option to return a result and a cancellation token.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <param name="returnResult">A flag indicating whether to return a result.</param>
    /// <returns>A task representing the asynchronous operation and optionally the result.</returns>
    Task<TResult> RunAsync(CancellationToken cancellationToken, bool returnResult = true);
}