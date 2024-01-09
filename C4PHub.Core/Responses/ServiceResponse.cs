using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Responses
{
    public class ServiceResponse<TValue>
    {
        public TValue Value { get; set; }

        public bool IsSuccess { get; set; }

        public string Error { get; set; }

        public override string ToString()
        {
            return $"IsSuccess={IsSuccess}; Error={Error}";
        }
    }
}
