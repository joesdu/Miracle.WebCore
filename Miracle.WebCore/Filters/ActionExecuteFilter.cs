using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Miracle.WebCore;
public class ActionExecuteFilter : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is null)
        {
            switch (context.Result)
            {
                case ObjectResult { Value: not null } result when result.Value.GetType().IsSubclassOf(typeof(Stream)):
                    break;
                case ObjectResult result:
                    context.Result = new ObjectResult(new { StatusCode = HttpStatusCode.OK, Msg = "success", Data = result.Value });
                    break;
                case EmptyResult:
                    context.Result = new ObjectResult(new { StatusCode = HttpStatusCode.OK, Msg = "success", Data = default(object) });
                    break;
            }
        }
        base.OnActionExecuted(context);
    }
}