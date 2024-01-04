using C4PHub.Core.Interfaces;
using C4PHub.Web.Models.C4P;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace C4PHub.Web.Controllers
{
    public class C4PController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IC4PManager _c4pManager;
        private readonly IC4PPersistance _persistance;

        public C4PController(IC4PManager c4pManager,
            ILogger<HomeController> logger, IC4PPersistance persistance)
        {
            _logger = logger;
            _c4pManager = c4pManager;
            _persistance = persistance;
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

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddModel model)
        {
            ////var url = "https://www.sessionize.com/future-tech-2024/";
            //var url = "https://www.papercall.io/serverlessdays-milano-2024";
            if (ModelState.IsValid)
            {
                var c4p = await _c4pManager.CreateC4PFromUrlAsync(model.Url, default);
                c4p.UserPublished = !User.Identity.IsAuthenticated ? model.UserPublished : User.Identity.Name;
                if (c4p.IsComplete())
                {
                    if (!await _persistance.ExistsC4PAsync(c4p, default))
                    {
                        if (await _persistance.SaveC4PAsync(c4p, default))
                        {
                            return RedirectToAction("Index", "Home");
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
                }
                else
                {
                    var completeModel = new CompleteModel();
                    completeModel.IsUserAuthenticated = User.Identity.IsAuthenticated;
                    if (completeModel.IsUserAuthenticated)
                        completeModel.UserPublished = User.Identity.Name;
                    else
                        completeModel.UserPublished = model.UserPublished;
                    completeModel.Url= model.Url;
                    return View("Complete", completeModel);
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
                    if (!await _persistance.ExistsC4PAsync(c4p, default))
                    {
                        if (await _persistance.SaveC4PAsync(c4p, default))
                        {
                            return RedirectToAction("Index", "Home");
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
                }
            }
            return View(model);
        }
    }
}
