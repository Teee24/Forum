namespace MiddlewareNFilter_Practice.MiddlewareTest
{
    public class MyMiddlewareTwo
    {
        private readonly RequestDelegate _next;
        public MyMiddlewareTwo(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            //自己寫的
            Console.WriteLine("MiddlewareTestTwo {IN} - 222");
            await _next(context);
            Console.WriteLine("MiddlewareTestTwo {Out} - 222");

        }
    }
    public static class MiddlewareTestTwoExtensions
    {
        //UseMiddlewareTest1是自己定義的,也就是等等app所呼叫的方法
        public static IApplicationBuilder UseMyMiddlewareTwo(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddlewareTwo>();
        }
    }
}
