using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Miracle.WebCore;
public class ExceptionMiddleware
{
    private readonly RequestDelegate next;
    public ExceptionMiddleware(RequestDelegate next) => this.next = next;
    public async Task Invoke(HttpContext context /* other dependencies */)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            StatusCode = HttpStatusCode.InternalServerError,
            Msg = ex.Message,
            Data = default(object)
        },
        typeof(object),
        new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        }));
    }
}

/// <summary>
/// 全局异常中间件
/// </summary>
public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalException(this IApplicationBuilder builder) => builder.UseMiddleware<ExceptionMiddleware>();
}