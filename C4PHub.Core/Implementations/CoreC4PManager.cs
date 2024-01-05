using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using C4PHub.Core.Responses;
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

        public CoreC4PManager(IUrlResolver urlResolver, IC4PExtractorFactory c4PExtractorFactory)
        {
            _c4pExtractorFactory = c4PExtractorFactory;
            _urlResolver = urlResolver;
        }

        public async Task<ServiceResponse<C4PInfo>> CreateC4PFromUrlAsync(string url, CancellationToken token = default)
        {
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
                var extractor = await _c4pExtractorFactory.GetExtractorAsync(response.Value, token);
                if (extractor != null)
                {
                    await extractor.FillC4PAsync(response.Value, token);
                }
            }
  
            return response;
        }
    }
}
