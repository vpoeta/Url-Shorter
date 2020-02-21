using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace UrlShorter.Services.Api.Common
{
    public class CustomResponseHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomResponseHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //To add Headers AFTER everything you need to do this
            //context.Response.OnStarting(state => {
            //    var httpContext = (HttpContext)state;
            //    httpContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //    httpContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept" });
            //    httpContext.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
            //    httpContext.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
            //    //httpContext.Response.Headers.Add("X-Response-Time-Milliseconds", new[] { watch.ElapsedMilliseconds.ToString() });

            //    return Task.CompletedTask;
            //}, context);

            context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            context.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "Origin, X-Requested-With, Content-Type, Accept" });
            context.Response.Headers.Add("Access-Control-Allow-Methods", new[] { "GET, POST, PUT, DELETE, OPTIONS" });
            context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
            await _next(context);
        }
    }

    public static class CustomResponseHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomResponseHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomResponseHandlerMiddleware>();
        }
    }
}
