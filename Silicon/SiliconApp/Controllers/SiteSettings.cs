using Microsoft.AspNetCore.Mvc;

namespace SiliconApp.Controllers
{
    public class SiteSettings : Controller
    {
        public IActionResult ChangeTheme(string theme) //Här tas theme parametern emot, som skickades från vår fetch
        {
            var options = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(60)
            };

            try
            {
                Response.Cookies.Append("ThemeMode", theme, options); //Lägg till cookien MED theme parametern

                return Ok();
            }

            catch //Om något skulle råka gå snett så får man bara ett 404, vilket inte kommer göra någonting mer i detta fallet än att inte gå vidare i vår JS funktion
            {
                return NotFound();
            }
        }
    }
}
