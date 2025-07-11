
namespace Backend.Shared.EndpointFilters;

public class LogginFilter<T>(ILogger<T> logger) : IEndpointFilter
{
    public ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var list = context.Arguments;

        logger.LogInformation("Logger works");
        return next(context);
    }
}
