using C4PHub.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace C4PHub.Web.Controllers
{
    [EnableRateLimiting("base")]
    [ApiController]
    [Route("feed")]
    public class RssController : ControllerBase
    {
        private readonly IFeedService _feed;

        public RssController(IFeedService feed)
        {
            _feed = feed;
        }

        [HttpGet(Name = "FeedRss")]
        [ResponseCache(Duration = 600)]
        public async Task<ContentResult> Feed()
        {
            string host = Request.Scheme + "://" + Request.Host;
            string contentType = "text/xml";

            var feedXml = await _feed.GenerateFeedAsync(host,default);

            return Content(feedXml, contentType);
        }
    }
}
