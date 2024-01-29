using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.OpenAI.Entities
{
    internal class C4PEntity
    {
        public string c4pUrl { get; set; }
        public string eventName { get; set; }
        public string eventStartDate { get; set; }
        public string eventEndDate { get; set; }
        public string c4pExpirationDate { get; set; }
        public string eventLocation { get; set; }
    }
}
