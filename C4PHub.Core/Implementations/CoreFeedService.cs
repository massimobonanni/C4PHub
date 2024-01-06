using C4PHub.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
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

            var feed = new SyndicationFeed("C4PHub", "C4PHub", new Uri(host), "C4PHub", DateTimeOffset.Now);

            var items = new List<SyndicationItem>();
            foreach (var c4p in c4pList)
            {
                var item = new SyndicationItem()
                {
                    Id = c4p.Id,
                    LastUpdatedTime = c4p.InsertDate,
                    PublishDate = c4p.InsertDate
                };
                item.Summary = SyndicationContent.CreatePlaintextContent($"Call for paper for the event '{c4p.EventName}' expires on {c4p.ExpiredDate:dd/MM/YYYY}");
                item.Content = SyndicationContent.CreatePlaintextContent($"Call for paper of the event '{c4p.EventName}' which will be held on {c4p.EventDate:dd/MM/YYYY} at {c4p.EventLocation} expires on {c4p.ExpiredDate:dd/MM/YYYY}");
                item.Links.Add(new SyndicationLink(new Uri(c4p.Url)));
                item.Authors.Add(new SyndicationPerson() { Name = c4p.UserPublished });

                items.Add(item);
            }
            feed.Items = items;

            StringWriter sw = new StringWriter();
            using (XmlWriter writer = XmlWriter.Create(sw, new XmlWriterSettings { Indent = true }))
            {
                feed.SaveAsRss20(writer);
            }

            return sw.ToString();
        }
    }
}
