using Backend.Shared.Auth;
using Backend.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Features.ResetPassword;


public record ResetDto(string Email, string OldPassword, string NewPassword);

public class ResetPassword : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
     => app.MapGroup("identity").MapPost("reset", async ([FromBody] ResetDto dto, AuthService authService) =>
     {
         var response = await authService.ResetPassword(dto);
         return response.IsSuccess ? Results.Ok("Password changed successfuly")
         : Results.BadRequest();

     }).WithTags("Identity")
     .Produces(StatusCodes.Status200OK)
     .ProducesProblem(StatusCodes.Status400BadRequest);



}
