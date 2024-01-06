using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Interfaces
{
    public interface IFeedService
    {
        Task<string> GenerateFeedAsync(string host,CancellationToken token);
    }
}
