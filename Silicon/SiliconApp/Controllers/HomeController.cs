using Microsoft.AspNetCore.Mvc;
using SiliconApp.Entities;
using SiliconApp.Services;
using SiliconApp.ViewModels;

namespace SiliconApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SubscriberService _subscriberService;

        public HomeController(SubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(HomeViewModel viewModel)
        {
            ViewData["Title"] = "Contact";

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var statusCode = await _subscriberService.Subscribe(viewModel.Form);

            switch (statusCode)
            {
                case "Created":
                    TempData["SuccessStatus"] = "Thanks for subscribing!";
                    break;
                case "Conflict":
                    TempData["ConflictStatus"] = "You are already subscribed.";
                    break;
                case "BadRequest":
                    TempData["ErrorStatus"] = "An error has occurred.";
                    break;
                case "Error":
                    TempData["ErrorStatus"] = "An error has occurred.";
                    break;
            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
