using Backend.Shared.Interfaces;

namespace Backend.Features.Login;

public record LoginDto(string UserName, string Password);

public class Login : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
    => app.MapPost("login", () => { })
    .WithGroupName("identity");
}
