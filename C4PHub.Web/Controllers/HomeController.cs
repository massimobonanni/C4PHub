using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using C4PHub.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace C4PHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IC4PExtractorFactory _c4pExtractorFactory;

        public HomeController(IC4PExtractorFactory c4pExtractorFactory,ILogger<HomeController> logger)
        {
            _logger = logger;
            _c4pExtractorFactory = c4pExtractorFactory;
        }

        public async Task<IActionResult> Index()
        {
            var c4p = new C4PInfo() { Url= "https://sessionize.com/web-day-2024/" };
            var extractor= await _c4pExtractorFactory.GetExtractorAsync(c4p,default);
            if (extractor!=null)
                await extractor.FillC4PAsync(c4p,default);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
