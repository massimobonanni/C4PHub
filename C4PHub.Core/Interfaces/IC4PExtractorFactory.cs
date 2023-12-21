using C4PHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Interfaces
{
    /// <summary>
    /// Represents a factory for creating C4P extractors.
    /// </summary>
    public interface IC4PExtractorFactory
    {
        /// <summary>
        /// Gets an extractor for the specified C4P information.
        /// </summary>
        /// <param name="c4p">The C4P information.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The task result contains the C4P extractor. The return value is null if no valid extractor is found.</returns>
        Task<IC4PExtractor> GetExtractorAsync(C4PInfo c4p, CancellationToken token = default);
    }
}
