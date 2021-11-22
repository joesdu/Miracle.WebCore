using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Miracle.WebApi.Filters;
public class ActionExecuteFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context) => base.OnActionExecuting(context);

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is null)
        {
            if (context.Result is ObjectResult result)
            {
                if (result.Value is null) context.Result = new ObjectResult(new { StatusCode = HttpStatusCode.OK, Msg = "success", Data = result.Value });
                else if (result.Value.GetType().IsSubclassOf(typeof(Stream))) { }
                else context.Result = new ObjectResult(new { StatusCode = HttpStatusCode.OK, Msg = "success", Data = result.Value });
            }
            else if (context.Result is EmptyResult)
            {
                context.Result = new ObjectResult(new { StatusCode = HttpStatusCode.OK, Msg = "success", Data = default(object) });
            }
        }
        base.OnActionExecuted(context);
    }
}