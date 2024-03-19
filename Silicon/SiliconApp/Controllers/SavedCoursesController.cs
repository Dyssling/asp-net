using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class SavedCoursesController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Active"] = "SavedCourses";

            return View();
        }
    }
}
