using C4PHub.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.EventGrid.Entities
{
    internal class EventData
    {
        public EventData(C4PInfo c4pInfo)
        {
            Url = c4pInfo.Url;
            EventName = c4pInfo.EventName;
            EventLocation = c4pInfo.EventLocation;
            EventDate = c4pInfo.EventDate;
            ExpiredDate = c4pInfo.ExpiredDate;
        }

        public string Url { get; set; }
        public string EventName { get; set; }
        public string EventLocation { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime ExpiredDate { get; set; }
    }
}
