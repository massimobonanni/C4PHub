using C4PHub.Core.Interfaces;
using C4PHub.RssApi.Writers;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
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

            var feed = new SyndicationFeed("C4PHub", "C4PHub", new Uri(host), "https://c4phub.com", DateTimeOffset.Now);

            feed.Categories.Add(new SyndicationCategory("Public Speaking"));
            feed.ImageUrl=new Uri("https://www.c4phub.com/img/c4phublogo.png");

            var items = new List<SyndicationItem>();
            foreach (var c4p in c4pList)
            {
                var item = new SyndicationItem()
                {
                    Id = c4p.Id,
                    LastUpdatedTime = c4p.InsertDate,
                    PublishDate = c4p.InsertDate
                };
                var summary= HttpUtility.HtmlEncode($"Call for paper for the event '{c4p.EventName}' expires on {c4p.ExpiredDate:dd/MM/yyyy}");
                item.Summary = SyndicationContent.CreatePlaintextContent(summary );
                var content= HttpUtility.HtmlEncode($"Call for paper of the event '{c4p.EventName}' which will be held on {c4p.EventDate:dd/MM/yyyy} at {c4p.EventLocation} expires on {c4p.ExpiredDate:dd/MM/yyyy}");
                item.Content = SyndicationContent.CreatePlaintextContent(content);
                item.Links.Add(new SyndicationLink(new Uri(c4p.Url)));
                item.Authors.Add(new SyndicationPerson() { Name = c4p.UserPublished });

                items.Add(item);
            }
            feed.Items = items;

            var sw = new StringWriterWithEncoding(Encoding.UTF8);
            using (XmlWriter writer = XmlWriter.Create(sw, new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 }))
            {
                feed.SaveAsRss20(writer);
            }

            return sw.ToString();
        }
    }
}
