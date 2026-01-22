using Microsoft.AspNetCore.Mvc.Filters;

namespace LvsMobileAPI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthorisation : Attribute, IAsyncActionFilter
    {
        // context is request
        // next Methode execute
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // check api auth
            //if (context.HttpContext.Request.Headers.TryGetValue("ApiKey", out var key))
            //{
            //    // steht aktuell noch in appsettings muss dann anschließend an den Login gekoppelt werden
            //    var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            //    var configApiKey = config.GetValue<string>("ApiKey");
            //    if (key.Equals(configApiKey) == false)
            //    {
            //        context.Result = new UnauthorizedResult();
            //        return;
            //    }
            //}
            //else
            //{
            //    context.Result = new UnauthorizedResult();
            //    return;
            //}
            await next();
        }
    }
}
