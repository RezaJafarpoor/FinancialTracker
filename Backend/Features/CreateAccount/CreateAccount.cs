using Backend.Shared;
using Backend.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

namespace Backend.Features.CreateAccount;

public record CreateAccountDto(string UserName, string Email, string Password);

public class CreateAccount : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
     => app.MapGroup("identity").MapPost("register", ([FromBody] CreateAccountDto dto, AuthService authService) =>
     {
         var response = authService.CreateUser(dto);
         return Results.Created();
     })
     .WithTags("Identity")
     .WithDescription("ساخت حساب کاربری")
     .Produces(StatusCodes.Status200OK)
     .ProducesProblem(StatusCodes.Status400BadRequest);




}