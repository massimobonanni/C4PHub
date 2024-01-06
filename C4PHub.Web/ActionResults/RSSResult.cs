using Microsoft.AspNetCore.Mvc;
using System.ServiceModel.Syndication;
using System.Xml;

namespace C4PHub.Web.ActionResults
{

    public class RSSResult : IActionResult
    {
        public SyndicationFeed feedData { get; set; }
        public string contentType = "rss";

        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.ContentType = "application/atom+xml";
            //check request is for Atom or RSS
            //if (context.HttpContext.Request.Query["type"] != null && context.HttpContext.Request.QueryString["type"].ToString().ToLower() == "atom")
            //{
            //    //Atom Feed
            //    context.HttpContext.Response.ContentType = "application/atom+xml";
            //    var rssFormatter = new Atom10FeedFormatter(feedData);
            //    using (XmlWriter writer = XmlWriter.Create(context.HttpContext.Response.Output, new XmlWriterSettings { Indent = true }))
            //    {
            //        rssFormatter.WriteTo(writer);
            //    }
            //}
            //else
            //{

            //RSS Feed
            context.HttpContext.Response.ContentType = "application/rss+xml";
            StringWriter sw = new StringWriter();
            using (XmlWriter writer = XmlWriter.Create(sw, new XmlWriterSettings { Indent = true }))
            {
                feedData.SaveAsRss20(writer);
            }
            var pippo= sw.ToString();
            return Task.CompletedTask;
        }
    }
}
