using Microsoft.AspNetCore.Mvc;
using SiliconApp.ViewModels;

namespace SiliconApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Home";

            return View();
        }

        [HttpPost]
        public IActionResult Index(HomeViewModel viewModel)
        {
            ViewData["Title"] = "Contact";

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            return RedirectToRoute(new { controller = "Account", action = "SignIn" });
        }
    }
}
