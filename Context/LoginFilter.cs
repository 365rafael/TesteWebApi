using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace TesteWebApi.Context
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
