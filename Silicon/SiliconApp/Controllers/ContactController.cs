using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Contact";

            return View();
        }
    }
}
