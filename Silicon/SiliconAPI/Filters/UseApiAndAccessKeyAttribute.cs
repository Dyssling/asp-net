using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SiliconAPI.Filters
{
    //Detta attributet är till för när man ska kräva Api nyckeln SAMT ännu en dold nyckel, för att ha ett extra säkerhetslager när vi kör GetToken action metoden

    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class UseApiAndAccessKeyAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>("ApiKey");
            var accessKey = configuration.GetValue<string>("AccessKey");

            if (!context.HttpContext.Request.Query.TryGetValue("key", out var providedKey) || !context.HttpContext.Request.Query.TryGetValue("access-key", out var providedAccessKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (apiKey != providedKey.ToString() || accessKey != providedAccessKey.ToString())
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
