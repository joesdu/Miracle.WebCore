using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Miracle.WebApi.Middlewares;
/// <summary>
/// 全局API耗时监控中间件
/// </summary>
public class ResponseTimeMiddleware
{
    private const string RESPONSE_HEADER_RESPONSE_TIME = "Miracle-Response-Time";
    private readonly RequestDelegate next;
    public ResponseTimeMiddleware(RequestDelegate next) => this.next = next;
    public async Task InvokeAsync(HttpContext context)
    {
        var watch = new Stopwatch();
        watch.Start();
        context.Response.OnStarting(() =>
        {
            watch.Stop();
            context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = $"{watch.ElapsedMilliseconds} ms";
            return Task.CompletedTask;
        });
        await next(context);
    }
}
/// <summary>
/// 全局API耗时监控中间件
/// </summary>
public static class ResponseTimeMiddlewareExtensions
{
    public static IApplicationBuilder UseResponseTime(this IApplicationBuilder builder) => builder.UseMiddleware<ResponseTimeMiddleware>();
}