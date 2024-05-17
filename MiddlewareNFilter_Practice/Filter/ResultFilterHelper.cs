using Microsoft.AspNetCore.Mvc.Filters;

namespace MiddlewareNFilter_Practice.Filter
{
    public class ResultFilterHelper : IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
