using Microsoft.AspNetCore.Mvc.Filters;

namespace MiddlewareNFilter_Practice.Filter
{
    public class AuthorizationFilterHelper : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.HttpContext.Response.WriteAsync($"{GetType().Name} in. \r\n");
        }
    }
}
