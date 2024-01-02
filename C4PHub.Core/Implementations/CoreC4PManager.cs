using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
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

        public CoreC4PManager(IUrlResolver urlResolver,IC4PExtractorFactory c4PExtractorFactory)
        {
            _c4pExtractorFactory = c4PExtractorFactory;
            _urlResolver = urlResolver;
        }

        public async Task<C4PInfo> CreateC4PFromUrlAsync(string url, CancellationToken token = default)
        {
            var c4pInfo = new C4PInfo();

            var resolvedUrl = await _urlResolver.ResolveUrlAsync(url, token);

            c4pInfo.Url = resolvedUrl;

            var extractor = await _c4pExtractorFactory.GetExtractorAsync(c4pInfo, token);
            if (extractor != null)
            {
                await extractor.FillC4PAsync(c4pInfo,token);
            }

            return c4pInfo;
        }
    }
}
