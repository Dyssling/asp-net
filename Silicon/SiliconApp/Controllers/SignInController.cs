using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class SignInController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
