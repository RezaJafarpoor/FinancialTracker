
using System.Diagnostics;
using System.Text;

namespace Backend.Shared.EndpointFilters;

public class LoggingFilter<T>(ILogger<T> logger) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var watch = new Stopwatch();
        watch.Start();
        var nextOne = await next(context);
        watch.Stop();
        if (watch.ElapsedMilliseconds > 1000)
        {
            logger.LogWarning(LogBuilder(watch.ElapsedMilliseconds, context));
            return nextOne;
        }

        logger.LogInformation(LogBuilder(watch.ElapsedMilliseconds, context));

        return nextOne;
    }


    private string LogBuilder(long milliseconds, EndpointFilterInvocationContext context)
    {
        var routeAddress = context.HttpContext.Request.Path.Value;
        var sb = new StringBuilder();
        sb.Append("url: ")
        .Append(routeAddress)
        .Append(" took: ")
        .Append(milliseconds)
        .Append(" ms.");
        return sb.ToString();
    }
}
