using C4PHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Interfaces
{
    /// <summary>
    /// Represents an interface for extracting C4P information.
    /// </summary>
    public interface IC4PExtractor
    {
        /// <summary>
        /// Checks if the C4P can be managed by the class.
        /// </summary>
        /// <param name="c4p">The C4PInfo object.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The task result contains a boolean value indicating if the C4P can be managed.</returns>
        Task<bool> CanManagedC4PAsync(C4PInfo c4p, CancellationToken token=default);

        /// <summary>
        /// Fills the C4P asynchronously.
        /// </summary>
        /// <param name="c4p">The C4PInfo object.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The task result contains a boolean value indicating if the C4P was successfully filled.</returns>
        Task<bool> FillC4PAsync(C4PInfo c4p, CancellationToken token=default);
    }
}
