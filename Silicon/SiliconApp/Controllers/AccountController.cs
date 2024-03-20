using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Details()
        {
            ViewData["Active"] = "Details"; //För att man sedan ska kunna sätta en active klass på rätt knapp

            return View();
        }

        public IActionResult Security()
        {
            ViewData["Active"] = "Security";

            return View();
        }

        public IActionResult SavedCourses()
        {
            ViewData["Active"] = "SavedCourses";

            return View();
        }

        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
    }
}
