using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class NotFoundController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
