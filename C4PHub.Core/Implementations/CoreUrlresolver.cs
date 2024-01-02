using C4PHub.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Implementations
{
    public class CoreUrlresolver : IUrlResolver
    {
        private readonly HttpClient _client;
        private readonly ILogger<CoreUrlresolver> _logger;
        public CoreUrlresolver(HttpClient client, ILogger<CoreUrlresolver> logger)
        {
            _client = client;
            _logger = logger;

        }

        public async Task<string> ResolveUrlAsync(string url, CancellationToken token = default)
        {
            string resolvedUrl = url;
            try
            {
                var response = await _client.GetAsync(url, token);
                var requestUri = response.RequestMessage.RequestUri;
                resolvedUrl = requestUri.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resolving url");
                throw;
            }
            return resolvedUrl;
        }
    }
}
