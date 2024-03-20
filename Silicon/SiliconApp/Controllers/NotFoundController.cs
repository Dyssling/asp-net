using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Page Not Found";

            return View();
        }
    }
}
