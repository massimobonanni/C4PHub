using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using C4PHub.Core.Responses;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Implementations
{
    public class CoreC4PManager : IC4PManager
    {
        private readonly IC4PExtractorFactory _c4pExtractorFactory;
        private readonly IUrlResolver _urlResolver;
        private readonly ILogger<CoreC4PManager> _logger;

        public CoreC4PManager(IUrlResolver urlResolver, IC4PExtractorFactory c4PExtractorFactory,
            ILoggerFactory loggerFactory)
        {
            _c4pExtractorFactory = c4PExtractorFactory;
            _urlResolver = urlResolver;
            _logger = loggerFactory.CreateLogger<CoreC4PManager>();
        }

        public async Task<ServiceResponse<C4PInfo>> CreateC4PFromUrlAsync(string url, CancellationToken token = default)
        {
            this._logger.LogInformation("Creating C4P from url {0}.", url);
            var response = new ServiceResponse<C4PInfo>()
            {
                IsSuccess = true,
                Value = new C4PInfo()
            };

            var resolvedUrl = await _urlResolver.ResolveUrlAsync(url, token);
            if (resolvedUrl == null)
            {
                response.IsSuccess = false;
                response.Error = "Error resolving url";
                response.Value = null;
            }
            else
            {
                response.Value.Url = resolvedUrl;
                this._logger.LogInformation("Resolved url {0} to {1}.", url, resolvedUrl);
                var extractor = await _c4pExtractorFactory.GetExtractorAsync(response.Value, token);
                if (extractor != null)
                {
                    this._logger.LogInformation("Extractor {0} found for url {1}.", extractor.GetType().Name, url);
                    await extractor.FillC4PAsync(response.Value, token);
                }
            }

            this._logger.LogInformation("Created C4P from url {0} with response {1}", url, response);
            return response;
        }
    }
}
