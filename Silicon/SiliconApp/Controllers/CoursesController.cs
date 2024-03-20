using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Courses";

            return View();
        }
    }
}
