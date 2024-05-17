using Microsoft.AspNetCore.Mvc.Filters;

namespace MiddlewareNFilter_Practice.Filter
{
    public class ExceptionFilterHelper : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
