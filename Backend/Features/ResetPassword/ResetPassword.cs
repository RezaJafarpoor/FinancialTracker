using System;
using Backend.Shared.Interfaces;

namespace Backend.Features.ResetPassword;

public class ResetPassword : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
     => app.MapPost("reset", () =>
     {

     }).WithGroupName("identity");

}
