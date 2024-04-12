using Microsoft.AspNetCore.Identity;
using SiliconApp.Entities;

namespace SiliconApp.Helpers.Middlewares
{
    public class UserSessionValidation
    {
        private readonly RequestDelegate _next;

        public UserSessionValidation(RequestDelegate next)
        {
            _next = next;
        }

        //private static bool IsAjaxRequest(HttpRequest request) => request.Headers.XRequestedWith == "XMLHttpRequest";

        public async Task InvokeAsync(HttpContext context, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            if (signInManager.IsSignedIn(context.User))
            {
                var user = await userManager.GetUserAsync(context.User);

                if (user == null)
                {
                    await signInManager.SignOutAsync();

                    context.Response.Redirect("/account/signin");
                    return;

                    //Nedanstående bortkommenterade kod var även med ursprungligen, men jag vill personligen att man ska loggas ut så fort användaren inte finns i databasen, vare sig det är en GET, POST, etc...

                    //if (!IsAjaxRequest(context.Request) && context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    context.Response.Redirect("/account/signin");
                    //    return;
                    //}
                }

            }

            await _next(context);
        }


    }
}
