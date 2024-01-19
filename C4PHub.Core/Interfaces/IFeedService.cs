using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Interfaces
{
    /// <summary>
    /// Represents a service for generating feeds.
    /// </summary>
    public interface IFeedService
    {
        /// <summary>
        /// Generates a feed asynchronously.
        /// </summary>
        /// <param name="host">The host of the feed.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated feed as a string.</returns>
        Task<string> GenerateFeedAsync(string host, CancellationToken token);
    }
}
