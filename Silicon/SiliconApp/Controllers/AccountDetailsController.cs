using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class AccountDetailsController : Controller
    {
        public IActionResult Details()
        {
            ViewData["Active"] = "Details"; //För att man sedan ska kunna sätta en active klass på rätt knapp

            return View();
        }
    }
}
