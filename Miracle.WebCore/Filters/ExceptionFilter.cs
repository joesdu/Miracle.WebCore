using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Miracle.WebCore;
public class ExceptionFilter : ExceptionFilterAttribute
{
    public override Task OnExceptionAsync(ExceptionContext context)
    {
        context.Result = new ObjectResult(new
        {
            StatusCode = HttpStatusCode.InternalServerError,
            Msg = context.Exception.Message,
            Data = default(object)
        });
        return base.OnExceptionAsync(context);
    }
}