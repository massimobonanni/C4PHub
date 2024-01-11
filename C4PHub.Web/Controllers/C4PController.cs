using C4PHub.Core.Entities;
using C4PHub.Core.Interfaces;
using C4PHub.Web.Models.C4P;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Xml;

namespace C4PHub.Web.Controllers
{
    [EnableRateLimiting("newC4P")]
    public class C4PController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IC4PManager _c4pManager;
        private readonly IC4PPersistance _persistance;
        private readonly INotificationService _notificationService;

        public C4PController(IC4PManager c4pManager,
            ILogger<HomeController> logger, IC4PPersistance persistance, INotificationService notificationService)
        {
            _logger = logger;
            _c4pManager = c4pManager;
            _persistance = persistance;
            _notificationService = notificationService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            var model = new AddModel();
            model.IsUserAuthenticated = User.Identity.IsAuthenticated;
            if (model.IsUserAuthenticated)
                model.UserPublished = User.Identity.Name;
            model.AddIfComplete = true;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddModel model)
        {
            model.IsUserAuthenticated = User.Identity.IsAuthenticated;
            if (model.IsUserAuthenticated)
                model.UserPublished = User.Identity.Name;

            if (ModelState.IsValid)
            {
                var response = await _c4pManager.CreateC4PFromUrlAsync(model.Url, default);

                if (!response.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, response.Error);
                }
                else
                {
                    var c4p = response.Value;
                    c4p.UserPublished = !User.Identity.IsAuthenticated ? model.UserPublished : User.Identity.Name;
                    if (await _persistance.ExistsC4PAsync(c4p, default))
                    {
                        ModelState.AddModelError(string.Empty, "C4P already exists");
                    }
                    else
                    {
                        if (model.AddIfComplete && c4p.IsComplete())
                        {
                            if (await SaveC4P(c4p))
                                return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            var completeModel = new CompleteModel();
                            completeModel.IsUserAuthenticated = User.Identity.IsAuthenticated;
                            if (completeModel.IsUserAuthenticated)
                                completeModel.UserPublished = User.Identity.Name;
                            else
                                completeModel.UserPublished = model.UserPublished;
                            completeModel.Url = model.Url;
                            completeModel.EventName = c4p.EventName;
                            completeModel.EventLocation = c4p.EventLocation;
                            completeModel.EventDate = c4p.EventDate;
                            completeModel.ExpiredDate = c4p.ExpiredDate;
                            return View("Complete", completeModel);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Complete(CompleteModel model)
        {
            if (ModelState.IsValid)
            {
                var c4p = new Core.Entities.C4PInfo();
                c4p.UserPublished = !User.Identity.IsAuthenticated ? model.UserPublished : User.Identity.Name;
                c4p.Url = model.Url;
                c4p.EventName = model.EventName;
                c4p.EventLocation = model.EventLocation;
                c4p.EventDate = model.EventDate.Value;
                c4p.ExpiredDate = model.ExpiredDate.Value;

                if (c4p.IsComplete())
                {
                    if (await SaveC4P(c4p))
                        return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        private async Task<bool> SaveC4P(C4PInfo c4p)
        {
            if (!await _persistance.ExistsC4PAsync(c4p, default))
            {
                if (await _persistance.SaveC4PAsync(c4p, default))
                {
                    await _notificationService.SendNotificationAsync(c4p, default);
                    return true;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error saving C4P");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "C4P already exists");
            }
            return false;
        }
    }
}
