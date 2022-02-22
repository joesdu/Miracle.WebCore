using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Miracle.WebCore;
/// <summary>
/// API耗时监控中间件
/// </summary>
public class ResponseTimeMiddleware
{
    private const string RESPONSE_HEADER_RESPONSE_TIME = "Miracle-Response-Time";
    private readonly RequestDelegate next;
    public ResponseTimeMiddleware(RequestDelegate next) => this.next = next;
    public async Task Invoke(HttpContext context)
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
    [Obsolete("请使用UseMiracleResponseTime注册管道,UseResponseTime在未来的版本中将会删除")]
    public static IApplicationBuilder UseResponseTime(this IApplicationBuilder builder) => builder.UseMiddleware<ResponseTimeMiddleware>();
    public static IApplicationBuilder UseMiracleResponseTime(this IApplicationBuilder builder) => builder.UseMiddleware<ResponseTimeMiddleware>();
}