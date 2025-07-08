using Backend.Shared;
using Backend.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Features.CreateAccount;

public record CreateAccountDto(string UserName, string Email, string Password);

public class CreateAccount : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
     => app.MapPost("", ([FromBody] CreateAccountDto dto, AuthService authService) =>
     {
         var response = authService.CreateUser(dto);
         return Results.Created();
     }).WithName("CreateAccount")
     .WithDescription("this endpoint is responsible to make and account");


}