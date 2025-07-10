using System.ComponentModel;
using Backend.Shared.Auth;
using Backend.Shared.Interfaces;
using Microsoft.Extensions.Options;

namespace Backend.Features.AuthFeature.Login;

public record LoginDto(string Email, string Password);

public class Login : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
    => app.MapGroup("identity").MapPost("login", async (HttpResponse httpResponse,
    IOptions<JwtSetting> setting,
     LoginDto dto,
      AuthService authService) =>
    {
        var token = await authService.Login(dto);
        if (!token.IsSuccess)
            return Results.Problem(
                detail: "Invalid username or password",
                statusCode: StatusCodes.Status401Unauthorized,
                title: "Unauthorized"
                );

        httpResponse.Cookies.Append("access_token", token.Data!, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(setting.Value.ExpirationTimeInMinute)
        });
        return Results.Ok();
    })
    .WithTags("Identity")
    .Produces(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status401Unauthorized);
}
