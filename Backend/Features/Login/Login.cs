using System.ComponentModel;
using Backend.Shared.Auth;
using Backend.Shared.Interfaces;
using Microsoft.Extensions.Options;

namespace Backend.Features.Login;

public record LoginDto(string UserName, string Password);

public class Login : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
    => app.MapGroup("identity").MapPost("login", async (HttpResponse httpResponse,
    IOptions<JwtSetting> setting,
     LoginDto dto,
      AuthService authService) =>
    {
        var token = await authService.Login(dto);
        if (token is null)
            return Results.Unauthorized();

        httpResponse.Cookies.Append("access_token", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(setting.Value.ExpirationTimeInMinute)
        });
        return Results.Ok();
    })
    .WithTags("Identity");
}
