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

        private readonly IC4PPersistance _persistance;

        public HomeController(ILogger<HomeController> logger, IC4PPersistance persistance)
        {
            _logger = logger;
            _persistance = persistance;
        }

        public async Task<IActionResult> Index()
        {
            ////var url = "https://www.sessionize.com/future-tech-2024/";
            //var url = "https://www.papercall.io/serverlessdays-milano-2024";

            //var c4p = await _c4pManager.CreateC4PFromUrlAsync(url, default);

            //if (!await _persistance.ExistsC4PAsync(c4p, default))
            //    await _persistance.SaveC4PAsync(c4p, default);
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
