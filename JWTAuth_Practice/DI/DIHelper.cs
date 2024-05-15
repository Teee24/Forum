using JWTAuth_Practice.JwtToken;

namespace JWTAuth_Practice.DI
{
    public static class DIHelper
    {
        public static void DIConfigurator(this IServiceCollection services)
        {
            //services.AddSingleton<JWTHelper>();
            services.AddScoped<JWTHelper>();
        }
    }
}
