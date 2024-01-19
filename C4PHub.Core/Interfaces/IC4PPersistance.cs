using C4PHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Interfaces
{
    /// <summary>
    /// Represents the interface for C4P persistence.
    /// </summary>
    public interface IC4PPersistance
    {
        /// <summary>
        /// Checks if a C4PInfo object exists asynchronously.
        /// </summary>
        /// <param name="c4p">The C4PInfo object to check.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating if the C4PInfo object exists.</returns>
        Task<bool> ExistsC4PAsync(C4PInfo c4p, CancellationToken token = default);

        /// <summary>
        /// Saves a C4PInfo object asynchronously.
        /// </summary>
        /// <param name="c4p">The C4PInfo object to save.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains a boolean value indicating if the C4PInfo object was saved successfully.</returns>
        Task<bool> SaveC4PAsync(C4PInfo c4p, CancellationToken token = default);

        /// <summary>
        /// Gets a collection of opened C4PInfo objects asynchronously.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The task result contains a collection of opened C4PInfo objects.</returns>
        Task<IEnumerable<C4PInfo>> GetOpenedC4PsAsync(CancellationToken token = default);

        /// <summary>
        /// Gets a collection of closed C4PInfo objects asynchronously.
        /// </summary>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The task result contains a collection of closed C4PInfo objects.</returns>
        Task<IEnumerable<C4PInfo>> GetClosedC4PsAsync(CancellationToken token = default);
    }
}
