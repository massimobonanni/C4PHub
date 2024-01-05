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

        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> Index()
        {
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
