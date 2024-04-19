using Microsoft.AspNetCore.Mvc;
using SiliconApp.Models;
using SiliconApp.Services;
using SiliconApp.ViewModels;

namespace SiliconApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactService _contactService;

        public ContactController(ContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Contact";

            return View(new ContactViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactViewModel viewModel)
        {
            ViewData["Title"] = "Contact";

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var statusCode = await _contactService.CreateRequest(viewModel.Form);

            switch (statusCode)
            {
                case "Created":
                    TempData["SuccessStatus"] = "We will get back to you shortly!";
                    break;
                case "BadRequest":
                    TempData["ErrorStatus"] = "An error has occurred.";
                    break;
                case "Error":
                    TempData["ErrorStatus"] = "An error has occurred.";
                    break;
            }

            return RedirectToRoute(new { controller = "Contact", action = "Index" });
        }
    }
}
