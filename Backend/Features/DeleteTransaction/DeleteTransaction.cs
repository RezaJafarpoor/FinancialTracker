using System.Security.Claims;
using Backend.Shared.Interfaces;
using Backend.Shared.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.DeleteTransaction;



public class DeleteTransaction : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
        => app.MapGroup("api")
        .MapDelete("transaction", async ([FromBody] Guid id, HttpContext context, ApplicationContext dbContext) =>
     {
         var token = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
         Guid.TryParse(token, out Guid userId);
         var deleteResult = await dbContext.Transactions.Where(t => t.UserId == userId & t.Id == id)
         .ExecuteDeleteAsync();
         return deleteResult > 0
         ? Results.NoContent()
         : Results.NotFound();
     }).WithTags("Transaction")
     .RequireAuthorization();
}
