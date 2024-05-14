using Microsoft.AspNetCore.Mvc.Filters;

namespace MiddlewareNFilter_Practice.Filter
{
    public class ActionFilter :Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("ActionFilter is executed.");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Executing AsyncActionFilter");
        }
    }
}
