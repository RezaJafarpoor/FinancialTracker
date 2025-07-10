using System.Security.Claims;
using Backend.Shared;
using Backend.Shared.Domain;
using Backend.Shared.Interfaces;
using Backend.Shared.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Features.TransactionFeature.CreateTransaction;

public record CreateTransactionDto(string IncomeType, int Amount, string Description, int Year, int Month, int Day, int Hour, int Minute);

public class CreateTransaction : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
         => app.MapGroup("api").MapPost("transaction",
         async ([FromBody] CreateTransactionDto dto, HttpContext context, ApplicationContext dbContext, DateTimeConverter timeConverter) =>
         {
             var userToken = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
             if (!Guid.TryParse(userToken, out Guid userId))
                 return Results.BadRequest();

             var dateTime = timeConverter.ConvertFromPersianCalenderToUtc(dto.Year, dto.Month, dto.Day, dto.Hour, dto.Minute);
             var transaction = Transaction.CreateTransaction(dto.IncomeType, dto.Amount, dto.Description, userId, dateTime);
             dbContext.Transactions.Add(transaction);
             if (await dbContext.SaveChangesAsync() > 0)
                 return Results.Created();
             return Results.BadRequest();
         }).WithTags("Transaction")
         .RequireAuthorization();
}
