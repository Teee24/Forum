namespace MiddlewareNFilter_Practice.MiddlewareTest
{
    public class MyMiddlewareThird
    {
        private readonly RequestDelegate _next;
        public MyMiddlewareThird(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            //自己寫的
            Console.WriteLine("MiddlewareTestOne {IN} - 333");
            await _next(context);
            Console.WriteLine("MiddlewareTestOne {Out} -333");

        }
    }
    public static class MyMiddlewareTestThirdExtensions
    {
        //UseMiddlewareTest1是自己定義的,也就是等等app所呼叫的方法
        public static IApplicationBuilder UseMiddlewareThird(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddlewareThird>();
        }
    }
}
