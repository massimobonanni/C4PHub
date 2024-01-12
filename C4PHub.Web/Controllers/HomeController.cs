using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using C4PHub.Web.Models;
using C4PHub.Web.Models.Home;
using C4PHub.Web.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Diagnostics;

namespace C4PHub.Web.Controllers
{
    [EnableRateLimiting("base")]
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
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> ActiveC4P()
        {
            var c4pList = await _persistance.GetOpenedC4PsAsync(default);
            var model = new ActiveC4PModel();
            model.C4PList = c4pList.OrderBy(c4p => c4p.ExpiredDate); ;
            return View(model);
        }

        [ResponseCache(Duration = 3600)]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = ErrorMessagesUtility.GetErrorMessage()
            });
        }
    }
}
