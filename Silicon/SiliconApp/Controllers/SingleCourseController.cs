using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class SingleCourseController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Single Course";

            return View();
        }
    }
}
