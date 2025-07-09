using Backend.Shared.Auth;
using Backend.Shared.Interfaces;

namespace Backend.Features.Login;

public record LoginDto(string UserName, string Password);

public class Login : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
    => app.MapGroup("identity").MapPost("login", async (LoginDto dto, AuthService authService) =>
    {
        var response = await authService.Login(dto);
        return response ? Results.Ok() : Results.BadRequest();
    })
    .WithTags("Identity");
}
