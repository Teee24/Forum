using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace MiddlewareNFilter_Practice.MiddlewareTest
{
    public class MiddlewareTest
    {
        private readonly RequestDelegate _next;
        public MiddlewareTest(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            //自己寫的
            Console.WriteLine("MiddlewareTest {IN} - 0");
            if (context.User.Identity.IsAuthenticated == false)
            {
             
            }
            await _next(context);
            Console.WriteLine("MiddlewareTest {Out} - 0");
        }
    }
    public static class MiddlewareTestThirdExtensions
    {
        //UseMiddlewareTest1是自己定義的,也就是等等app所呼叫的方法
        public static IApplicationBuilder UseMiddlewareTest(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiddlewareTest>();
        }
    }
}
