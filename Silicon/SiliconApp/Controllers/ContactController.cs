using Microsoft.AspNetCore.Mvc;
using SiliconApp.Models;
using SiliconApp.ViewModels;

namespace SiliconApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Contact";

            return View(new ContactViewModel());
        }

        [HttpPost]
        public IActionResult Index(ContactViewModel viewModel)
        {
            ViewData["Title"] = "Contact";

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            return RedirectToRoute(new { controller = "Contact", action = "Index" });
        }
    }
}
