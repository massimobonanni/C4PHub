using C4PHub.Core.Entities;
using C4PHub.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Interfaces
{
    /// <summary>
    /// Represents the C4PManager interface.
    /// </summary>
    public interface IC4PManager
    {
        /// <summary>
        /// Creates a C4P from a given URL asynchronously.
        /// </summary>
        /// <param name="url">The URL to create the C4P from.</param>
        /// <param name="token">The cancellation token (optional).</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the service response with the created C4P information.</returns>
        Task<ServiceResponse<C4PInfo>> CreateC4PFromUrlAsync(string url, CancellationToken token = default);
    }
}
