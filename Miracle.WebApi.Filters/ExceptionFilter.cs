using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Miracle.WebApi.Filters;
public class ExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        context.Result = new ObjectResult(new { StatusCode = HttpStatusCode.InternalServerError, Msg = context.Exception.Message, Data = default(object) });
        base.OnException(context);
    }

    public override async Task OnExceptionAsync(ExceptionContext context)
    {
        OnException(context);
        await base.OnExceptionAsync(context);
    }
}