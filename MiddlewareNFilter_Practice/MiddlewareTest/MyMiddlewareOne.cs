using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace MiddlewareNFilter_Practice.MiddlewareTest
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MyMiddlewareOne
    {
        private readonly RequestDelegate _next;

        public MyMiddlewareOne(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //用產出的
            Console.WriteLine("[IN]This is MyMiddlewareTest  -111 ");
            await _next(httpContext);
            Console.WriteLine("[Out]This is MyMiddlewareTest  -111 ");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MyMiddlewareOneExtensions
    {
        public static IApplicationBuilder UseMyMiddlewareOne(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddlewareOne>();
        }
    }
}
