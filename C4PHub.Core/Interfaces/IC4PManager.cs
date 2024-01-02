using C4PHub.Core.Entities;
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
        Task<C4PInfo> CreateC4PFromUrlAsync(string url, CancellationToken token = default);
    }
}
