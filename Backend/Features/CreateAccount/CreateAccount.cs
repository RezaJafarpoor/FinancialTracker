using Backend.Shared;
using Backend.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Features.CreateAccount;

public record CreateAccountDto(string UserName, string Email, string Password);

public class CreateAccount : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
     => app.MapPost("/register", ([FromBody] CreateAccountDto dto, AuthService authService) =>
     {
         var response = authService.CreateUser(dto);
         return Results.Created();
     })
     .WithGroupName("identity")
     .WithName("CreateAccount")
     .WithDescription("this endpoint is responsible to make and account");


}