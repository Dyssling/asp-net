using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Details()
        {
            ViewData["Active"] = "Details"; //För att man sedan ska kunna sätta en active klass på rätt knapp
            ViewData["Title"] = "Account Details";

            return View();
        }

        public IActionResult Security()
        {
            ViewData["Active"] = "Security";
            ViewData["Title"] = "Account Security";

            return View();
        }

        public IActionResult SavedCourses()
        {
            ViewData["Active"] = "SavedCourses";
            ViewData["Title"] = "Saved Courses";

            return View();
        }

        public IActionResult SignIn()
        {
            ViewData["Title"] = "Sign In";

            return View();
        }

        public IActionResult SignUp()
        {
            ViewData["Title"] = "Sign Up";

            return View();
        }
    }
}
