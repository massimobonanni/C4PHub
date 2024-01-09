using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Implementations
{
    /// <summary>
    /// Represents a factory for creating IC4PExtractor instances.
    /// </summary>
    public class CoreExtractorFactory : IC4PExtractorFactory
    {
        private readonly IEnumerable<IC4PExtractor> _c4pExtractors;
        private readonly ILogger<CoreExtractorFactory> _logger;

        public CoreExtractorFactory(IServiceProvider serviceProvider,ILoggerFactory loggerFactory)
        {
            _c4pExtractors = serviceProvider.GetKeyedServices<IC4PExtractor>("extractors");
            _logger = loggerFactory.CreateLogger<CoreExtractorFactory>();
        }

        /// <summary>
        /// Gets the appropriate IC4PExtractor for the given C4PInfo.
        /// </summary>
        /// <param name="c4p">The C4PInfo object.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>The IC4PExtractor instance.</returns>
        public async Task<IC4PExtractor> GetExtractorAsync(C4PInfo c4p, CancellationToken token = default)
        {
            foreach (var extractor in _c4pExtractors)
            {
                if (await extractor.CanManagedC4PAsync(c4p, token))
                    return extractor;
            }
            return null;
        }
    }
}
