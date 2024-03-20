using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Home";

            return View();
        }
    }
}
