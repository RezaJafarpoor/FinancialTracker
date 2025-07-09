using Backend.Shared.Auth;
using Backend.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;

namespace Backend.Features.CreateAccount;

public record CreateAccountDto(string UserName, string Email, string Password);

public class CreateAccount : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
     => app.MapGroup("identity").MapPost("register", async ([FromBody] CreateAccountDto dto, AuthService authService) =>
     {
         var response = await authService.CreateUser(dto);
         return response ? Results.Created() : Results.BadRequest();

     }).WithTags("Identity")
     .WithDescription("ساخت حساب کاربری")
     .Produces(StatusCodes.Status200OK)
     .ProducesProblem(StatusCodes.Status400BadRequest);




}