using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Interfaces
{
    public interface IUrlResolver
    {
        Task<string> ResolveUrlAsync(string url, CancellationToken token = default);
    }
}
