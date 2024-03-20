using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")] //Alla URL:er som har /error följt av en parameter, dirigeras hit. Parametern plockas upp i Index metoden i form av en parameter vid namn statusCode
        public IActionResult Index(int? statusCode)
        {
            if (statusCode.HasValue)
            {
                if (statusCode == 404) //Om det är en 404 error så hamnar man på 404 vyn
                {
                    ViewData["Title"] = "Page Not Found";

                    return View("NotFound");
                }
                else //Annars omdirigeras man tillbaka till startsidan
                {
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }

            }

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }
}
