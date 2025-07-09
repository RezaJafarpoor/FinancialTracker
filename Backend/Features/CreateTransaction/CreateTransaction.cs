using System.Security.Claims;
using Backend.Shared.Domain;
using Backend.Shared.Interfaces;
using Backend.Shared.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Features.CreateTransaction;

public record CreateTransactionDto(string IncomeType, int Amount, string Description);

public class CreateTransaction : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
         => app.MapGroup("transaction").MapPost("/transaction",
         async ([FromBody] CreateTransactionDto dto, HttpContext context, ApplicationContext dbContext) =>
         {
             var userToken = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
             if (!Guid.TryParse(userToken, out Guid userId))
                 return Results.BadRequest();
             var transaction = Transaction.CreateTransaction(dto.IncomeType, dto.Amount, dto.Description, userId);
             dbContext.Transactions.Add(transaction);
             if (await dbContext.SaveChangesAsync() > 0)
                 return Results.Created();
             return Results.BadRequest();

         }).WithTags("Transaction")
         .RequireAuthorization();
}
