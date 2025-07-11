using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace Backend.Shared.Extensions;

public static class ExceptionHandlerExtension
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
        => app.UseExceptionHandler(error =>
        error.Run(async context =>
        {
            var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("GlobalExeptionHandler");
            var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
            var endpointPath = context.Features.Get<IExceptionHandlerFeature>()?.Path;

            logger.LogCritical($" the {endpointPath} threw exception : {exception?.Message}");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { error = "InternalServerError" });
        }));
}
