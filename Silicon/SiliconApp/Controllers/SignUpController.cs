using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
