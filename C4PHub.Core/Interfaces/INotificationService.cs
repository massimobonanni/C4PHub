using C4PHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Interfaces
{
    public interface INotificationService
    {
        Task SendNotificationAsync(C4PInfo c4pInfo, CancellationToken cancellationToken = default);
    }
}
