using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;

namespace C4PHub.Core.Implementations
{
    public class CoreFeedService : IFeedService
    {
        private readonly IC4PPersistance _persistance;

        public CoreFeedService(IC4PPersistance persistance)
        {
            _persistance = persistance;
        }

        public async Task<string> GenerateFeedAsync(string host, CancellationToken token)
        {
            var c4pList = await _persistance.GetOpenedC4PsAsync(token);

            c4pList = c4pList.OrderByDescending(c => c.InsertDate);

            var feed = new SyndicationFeed("C4PHub",
                "The platform to share C4P with your community!", new Uri("https://c4phub.com"),
                "https://rss.c4phub.com/feed", DateTime.Now);
            feed.Copyright = new TextSyndicationContent($"{DateTime.Now.Year} C4PHub");
            feed.Language = "en-US";

            var items = new List<SyndicationItem>();
            foreach (var c4pInfo in c4pList)
            {
                var postUrl = c4pInfo.Url;
                var title = c4pInfo.EventName;
                var description = $"Call for paper of the event '{c4pInfo.EventName}' which will be held on {c4pInfo.EventDate:dd/MM/yyyy} at {c4pInfo.EventLocation} expires on {c4pInfo.ExpiredDate:dd/MM/yyyy}";
                items.Add(new SyndicationItem(title, description, new Uri(postUrl),c4pInfo.Id, c4pInfo.InsertDate));
            }
            feed.Items = items;

            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                NewLineHandling = NewLineHandling.Entitize,
                NewLineOnAttributes = true,
                Indent = true
            };
            using (var stream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(stream, settings))
                {
                    var rssFormatter = new Rss20FeedFormatter(feed, false);
                    rssFormatter.WriteTo(xmlWriter);
                    xmlWriter.Flush();
                }
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
