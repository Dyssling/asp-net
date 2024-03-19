using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class AccountSecurityController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Active"] = "Security";

            return View();
        }
    }
}
