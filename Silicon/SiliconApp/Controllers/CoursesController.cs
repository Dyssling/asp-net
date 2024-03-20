using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
