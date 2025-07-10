using System.Security.Claims;
using Backend.Shared.Interfaces;
using Backend.Shared.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.UpdateTransaction;

public record UpdateTransactionDto(Guid Id, string? IncomeType, int? Amount, string? Description);


public class UpdateTrasaction : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
     => app.MapGroup("api")
     .MapPut("transaction", async ([FromBody] UpdateTransactionDto dto, HttpContext context, ApplicationContext dbContext) =>
     {
         var token = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
         Guid.TryParse(token, out var userId);
         var transaction = await dbContext.Transactions
            .FirstOrDefaultAsync(t => t.UserId == userId & t.Id == dto.Id);
         if (transaction is null)
             return Results.NotFound();

         transaction.UpdateTrasaction(dto.IncomeType, dto.Amount, dto.Description);
         if (await dbContext.SaveChangesAsync() > 0)
             return Results.NoContent();

         return Results.BadRequest();
     }).WithTags("Transaction")
     .RequireAuthorization();

}
