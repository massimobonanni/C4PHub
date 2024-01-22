using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Reflection;

namespace C4PHub.RssApi.Controllers
{
    [EnableRateLimiting("base")]
    [ApiController]
    [Route("version")]
    public class VersionController : Controller
    {
        [HttpGet(Name = "Version")]
        [ResponseCache(Duration = 60)]
        public ContentResult Version()
        {
            string contentType = "text/plain; charset=utf-8";

            var version = $"Version {@Assembly.GetEntryAssembly().GetName().Version}";

            return Content(version, contentType);
        }
    }
}
