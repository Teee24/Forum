using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;

namespace HangfirePractice;

public class MyAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();
        return httpContext.User.Identity.IsAuthenticated;
    }
}