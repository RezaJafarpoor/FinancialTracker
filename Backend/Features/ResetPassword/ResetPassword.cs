using System;
using Backend.Shared.Interfaces;

namespace Backend.Features.ResetPassword;

public class ResetPassword : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
     => app.MapGroup("identity").MapPost("reset", () =>
     {

     }).WithTags("Identity");


}
