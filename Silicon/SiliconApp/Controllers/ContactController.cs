using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
