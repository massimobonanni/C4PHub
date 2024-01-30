using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using Ical.Net;
using Ical.Net.CalendarComponents;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Microsoft.Extensions.Logging;
using System.ComponentModel;
using System.Diagnostics.Tracing;

namespace C4PHub.Calendar.ICalNet.Implementations
{
    public class CalNetCalendarGenerator : IC4PCalendarGenerator
    {
        private readonly ILogger<CalNetCalendarGenerator> _logger;

        public CalNetCalendarGenerator(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CalNetCalendarGenerator>();
        }

        public Task<string> GenerateAsync(C4PInfo c4p, CalendarFormat format, CalendarType type, CancellationToken cancellationToken = default)
        {
            switch (format)
            {
                case CalendarFormat.Ics:
                    return GenerateIcsAsync(c4p, type, cancellationToken);
                default:
                    throw new NotSupportedException();
            }
        }

        public Task<string> GenerateIcsAsync(C4PInfo c4p, CalendarType type, CancellationToken cancellationToken = default)
        {
            var @event = new CalendarEvent()
            {
                Location = c4p.EventLocation.GetFirstChars(75),
            };

            switch (type)
            {
                case CalendarType.EventDate:
                    @event.Description = $"{c4p.EventName}";
                    @event.Summary = $"{c4p.EventName}";
                    @event.Start = new CalDateTime(c4p.EventDate.ToStandardFormatString());
                    @event.End = new CalDateTime(c4p.EventEndDate.ToStandardFormatString());
                    break;
                case CalendarType.C4PExpirationDate:
                    @event.Description = $"C4P expiration for {c4p.EventName}";
                    @event.Summary = $"C4P expiration for {c4p.EventName}";
                    @event.Start = new CalDateTime(c4p.ExpiredDate.ToStandardFormatString());
                    @event.Duration = new TimeSpan(24, 0, 0);
                    break;
                default:
                    throw new NotImplementedException();
            }
            var calendar = new Calendar();
            calendar.Events.Add(@event);

            var serializer = new CalendarSerializer();
            var serializedCalendar = serializer.SerializeToString(calendar);
            return Task.FromResult(serializedCalendar);
        }
    }
}
