using C4PHub.Core.Interfaces;
using C4PHub.Web.ActionResults;
using Microsoft.AspNetCore.Mvc;

namespace C4PHub.Web.Controllers
{
    public class RssController : Controller
    {
        private readonly IFeedService _feed;

        public RssController(IFeedService feed)
        {
            _feed = feed;
        }

        [ResponseCache(Duration = 600)]
        public async Task<IActionResult> Feed()
        {
            string host = Request.Scheme + "://" + Request.Host;
            string contentType = "application/xml";

            var feedXml = await _feed.GenerateFeedAsync(host,default);

            return Content(feedXml, contentType);
        }
    }
}
