using Application_Core.Exception;

namespace WebAPI.ExceptionFilter;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    // filter order execution (ascending)
    // we make sure that this filter will be executed after all of other filters
    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // check if exception is our custom exception
        if (context.Exception is not HttpResponseException httpResponseException) 
            return;

        // change the response result to custom object so we can avoid revealing stacktrace to the client
        context.Result = new ObjectResult(new { message = httpResponseException.Message, code = (int) httpResponseException.GetStatusCode() })
        {
            StatusCode = (int) httpResponseException.GetStatusCode()
        };
        
        context.ExceptionHandled = true;
    }
}