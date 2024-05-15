using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace JWTAuth_Practice.TestMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyMiddlewareBefore
    {
        private readonly RequestDelegate _next;

        public MyMiddlewareBefore(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("After: " + httpContext.User.Identity.IsAuthenticated);
            return _next(httpContext);
        
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareBeforeExtensions
    {
        public static IApplicationBuilder UseMyMiddlewareBefore(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddlewareBefore>();
        }
    }
}
