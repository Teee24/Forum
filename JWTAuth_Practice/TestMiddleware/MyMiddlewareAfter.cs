using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace JWTAuth_Practice.TestMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyMiddlewareAfter
    {
        private readonly RequestDelegate _next;

        public MyMiddlewareAfter(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("Before: "+ httpContext.User.Identity.IsAuthenticated);
            return _next(httpContext);
          
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareAfterExtensions
    {
        public static IApplicationBuilder UseMyMiddlewareAfter(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddlewareAfter>();
        }
    }
}
