using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using C4PHub.RssApi.Writers;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
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

            StringWriter sw = new StringWriter();
            using (XmlWriter xmlWriter = XmlWriter.Create(sw,
                new XmlWriterSettings() { Async = true, Indent = true }))
            {
                var rss = new RssFeedWriter(xmlWriter);
                await rss.WriteTitle("C4PHub");
                await rss.WriteDescription("The platform to share C4P with your community!");
                await rss.WriteGenerator("C4PHub");
                await rss.WriteValue("link", host);

                if (c4pList != null && c4pList.Count() > 0)
                {
                    var feedItems = new List<AtomEntry>();
                    foreach (var c4p in c4pList)
                    {
                        var item = ToRssItem(c4p, host);
                        feedItems.Add(item);
                    }

                    foreach (var feedItem in feedItems)
                    {
                        await rss.Write(feedItem);
                    }
                }
            }
            return sw.ToString();
        }

        private AtomEntry ToRssItem(C4PInfo c4pInfo, string host)
        {
            var item = new AtomEntry
            {
                Title = c4pInfo.EventName),
                Description = $"Call for paper of the event '{c4pInfo.EventName}' which will be held on {c4pInfo.EventDate:dd/MM/yyyy} at {c4pInfo.EventLocation} expires on {c4pInfo.ExpiredDate:dd/MM/yyyy}",
                Id = c4pInfo.Id,
                Published = c4pInfo.InsertDate,
                LastUpdated = c4pInfo.InsertDate,
                ContentType = "html",
            };

            item.AddContributor(new SyndicationPerson(c4pInfo.UserPublished, c4pInfo.UserPublished));

            item.AddLink(new SyndicationLink(new Uri(c4pInfo.Url)));

            return item;
        }
    }
}
