using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Core.Implementations
{
    public class CoreC4PCalendarGenerator : IC4PCalendarGenerator
    {
        private class iCalendar
        {
            public DateTimeOffset EventStartDateTime { get; set; }
            public DateTimeOffset EventEndDateTime { get; set; }
            public DateTimeOffset EventTimeStamp { get; set; }
            public DateTimeOffset EventCreatedDateTime { get; set; }
            public DateTimeOffset EventLastModifiedTimeStamp { get; set; }
            public string UID { get; set; }
            public string EventDescription { get; set; }
            public string EventLocation { get; set; }
            public string EventSummary { get; set; }

            public iCalendar(C4PInfo c4p, CalendarType type)
            {
                EventTimeStamp = DateTime.Now;
                EventCreatedDateTime = EventTimeStamp;
                EventLastModifiedTimeStamp = EventTimeStamp;
                UID = Guid.NewGuid().ToString();
                switch (type)
                {
                    case CalendarType.EventDate:
                        EventDescription = $"{c4p.EventName}";
                        EventStartDateTime = c4p.EventDate;
                        EventEndDateTime = c4p.EventDate;
                        break;
                    case CalendarType.C4PExpirationDate:
                        EventDescription = $"C4P for {c4p.EventName} expiration";
                        EventStartDateTime = c4p.ExpiredDate;
                        EventEndDateTime = c4p.ExpiredDate;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                EventLocation = c4p.EventLocation;
                EventSummary = c4p.EventName;
            }
        }

        private readonly ILogger<CoreC4PCalendarGenerator> _logger;

        public CoreC4PCalendarGenerator(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CoreC4PCalendarGenerator>();
        }

        public Task<string> GenerateAsync(C4PInfo c4p, CalendarFormat format, CalendarType type, CancellationToken cancellationToken = default)
        {
            switch (format)
            {
                case CalendarFormat.Ics:
                    return GenerateIcsAsync(c4p, type, cancellationToken);
                default:
                    throw new NotImplementedException();
            }
        }

        public Task<string> GenerateIcsAsync(C4PInfo c4p, CalendarType type, CancellationToken cancellationToken = default)
        {
            var iCal=new iCalendar(c4p, type);

            StringBuilder sb = new StringBuilder();
            //Calendar
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("PRODID:-//C4PHub.com//C4PHub MIMEDIR//EN");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("METHOD:PUBLISH");
            //sb.AppendLine("BEGIN: VTIMEZONE");
            //sb.AppendLine("TZID:UTC");
            //sb.AppendLine("END:VTIMEZONE");
            //Event
            sb.AppendLine("BEGIN:VEVENT");
            sb.AppendLine("CLASS:PUBLIC");
            sb.AppendLine("DTSTART:" + iCal.EventStartDateTime.ToUniversalTime().ToString("yyyyMMdd")+ "T000000Z");
            sb.AppendLine("DTEND:" + iCal.EventStartDateTime.ToUniversalTime().ToString("yyyyMMdd")+ "T235959Z");
            sb.AppendLine("DTSTAMP:" + iCal.EventTimeStamp.ToUniversalTime().ToString("yyyyMMddHHmmssZ"));
            sb.AppendLine("ORGANIZER;CN=C4PHub.com");
            sb.AppendLine("UID:" + iCal.UID);
            sb.AppendLine("CREATED:" + iCal.EventCreatedDateTime.ToString("yyyyMMddHHmmssZ"));
            sb.AppendLine("LAST-MODIFIED:" + iCal.EventLastModifiedTimeStamp.ToUniversalTime().ToString("yyyyMMddHHmmssZ"));
            sb.AppendLine("LOCATION:" + iCal.EventLocation.Substring(0,Math.Min(iCal.EventLocation.Length, 75)));
            sb.AppendLine("SEQUENCE:0");
            switch (type)
            {
                case CalendarType.EventDate:
                    sb.AppendLine("DESCRIPTION:" + iCal.EventSummary);
                    sb.AppendLine("SUMMARY;LANGUAGE=en:" + iCal.EventSummary);
                    break;
                case CalendarType.C4PExpirationDate:
                    sb.AppendLine("DESCRIPTION:C4P Expiration for " + iCal.EventSummary);
                    sb.AppendLine("SUMMARY;LANGUAGE=en:C4P Expiration for " + iCal.EventSummary);
                    break;
                default:
                    break;
            }
            sb.AppendLine("TRANSP:TRANSPARENT");
            sb.AppendLine("X-MICROSOFT-CDO-BUSYSTATUS:FREE");
            sb.AppendLine("END:VEVENT");
            sb.AppendLine("END:VCALENDAR");
            
            return Task.FromResult(sb.ToString());
        }
    }
}
