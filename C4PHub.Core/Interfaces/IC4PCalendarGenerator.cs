using C4PHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Interfaces
{
    public interface IC4PCalendarGenerator
    {
        Task<string> GenerateAsync(C4PInfo c4p, CalendarFormat format, CalendarType type, CancellationToken cancellationToken = default);
    }
}
