using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using C4PHub.Core.Utilities;
using C4PHub.Sessionize.Utilities;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4PHub.Sessionize.Implementations
{
    public class SessionizeC4PExtractor : IC4PExtractor
    {
        private static readonly string[] _validUrlPrefixes = new string[]
        {
            "https://sessionize.com",
            "https://www.sessionize.com"
        };

        private readonly ILogger<SessionizeC4PExtractor> _logger;

        public SessionizeC4PExtractor(ILogger<SessionizeC4PExtractor> logger)
        {
            _logger = logger;
        }

        public Task<bool> CanManagedC4PAsync(C4PInfo c4p, CancellationToken token)
        {
            this._logger.LogInformation("Checking if Sessionize page {0} can be managed.", c4p.Url);
            string url = c4p.Url;
            bool startsWithValidPrefix = _validUrlPrefixes.Any(prefix => url.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase));
            this._logger.LogInformation("Sessionize page {0} can be managed: {1}.", c4p.Url, startsWithValidPrefix);
            return Task.FromResult(startsWithValidPrefix);
        }

        public async Task<bool> FillC4PAsync(C4PInfo c4p, CancellationToken token)
        {
            this._logger.LogInformation("Extracting C4P information from Sessionize page {0}.", c4p.Url);

            HtmlWeb web = new HtmlWeb();

            try
            {
                var htmlDoc = await web.LoadFromWebAsync(c4p.Url);

                var rightColumn = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"right-column\"]");
                var leftColumn = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"left-column\"]");

                // Extract Event Title
                var titleNode = leftColumn.SelectSingleNode(".//div[@class='ibox-title']/h4");
                c4p.EventName = System.Net.WebUtility.HtmlDecode(titleNode.InnerText);

                var contentNode = leftColumn.SelectSingleNode(".//div[@class='ibox-content']");

                var contentRows = contentNode.SelectNodes(".//div[@class='row']");

                // Extract Event Start Date
                var eventDatesRow = contentRows[0];
                var eventStartDateNode = eventDatesRow.SelectSingleNode(".//h2");
                var eventStartDateStr = eventStartDateNode.InnerText;
                var eventEndDateStr = eventStartDateStr;

                // Extract Event End Date
                var eventEndDateNode = eventDatesRow.SelectSingleNode(".//div[2]/h2");
                if (eventEndDateNode != null)
                {
                    eventEndDateStr = eventEndDateNode.InnerText;
                }

                // Extract location
                var locationDateRow = contentRows[1];
                var locationNode = locationDateRow.SelectSingleNode(".//h2");
                c4p.EventLocation = System.Net.WebUtility.HtmlDecode(Utility.CleanInnerText(locationNode.InnerText));

                contentNode = rightColumn.SelectSingleNode(".//div[@class='ibox-content']");

                contentRows = contentNode.SelectNodes(".//div[@class='row']");
                // Extract C4P closing date
                var c4pClosingDateRow = contentRows[0];

                var c4pclosingTimeNode = c4pClosingDateRow.SelectSingleNode(".//div[2]/div[1]");
                var c4pClosingTime = StringUtility.ExtractAMPMPart(c4pclosingTimeNode.InnerText);
                var c4pClosingDateNode = c4pClosingDateRow.SelectSingleNode(".//div[2]/h2");
                var c4pClosingDate = c4pClosingDateNode.InnerText;

                var c4pClosingDateTimezoneRow = contentRows[1];
                var c4pClosingDateTimezoneNode = c4pClosingDateTimezoneRow.SelectSingleNode(".//div[1]/small/strong"); // UTC +01:00
                var c4pClosingDateTimezone = StringUtility.ExtractUTCPart(c4pClosingDateTimezoneNode.InnerText);

                var c4pClosingDateTimeOffset = DateTimeUtility.ParseStringToDateTimeOffset($"{c4pClosingDate} {c4pClosingTime} {c4pClosingDateTimezone}");
                if (c4pClosingDateTimeOffset.HasValue)
                    c4p.ExpiredDate = c4pClosingDateTimeOffset.Value;

                var eventStartDate= DateTimeUtility.ParseStringToDateTimeOffset($"{eventStartDateStr} 00:00 AM UTC+00:00"); 
                if (eventStartDate.HasValue)
                    c4p.EventDate = eventStartDate.Value;

                var eventEndDate = DateTimeUtility.ParseStringToDateTimeOffset($"{eventEndDateStr} 11:59 PM UTC+00:00");
                if (eventEndDate.HasValue)
                    c4p.EventEndDate = eventEndDate.Value;

                this._logger.LogInformation("C4P information extracted from Sessionize page {0}.", c4p.Url);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while extracting C4P information from Sessionize page {0}.", c4p.Url);
                return false;
            }
        }
    }
}
