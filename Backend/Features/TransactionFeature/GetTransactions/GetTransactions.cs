using System.Security.Claims;
using Backend.Features.TransactionFeature;
using Backend.Shared.Interfaces;
using Backend.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.GetTransaction;

public record TransactionDto(Guid Id, string IncomeType, int Amount, string Date, string Time, string Description);
public class GetTransactions : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
        => app.MapGroup("api")
        .MapGet("transaction", async (HttpContext context, ApplicationContext dbContext, TransactionMapper mapper) =>
        {
            var token = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(token, out Guid userId);
            var transactions = await dbContext.Transactions.Where(t => t.UserId == userId).ToListAsync();
            return transactions is null
            ? Results.NotFound()
            : Results.Ok(mapper.MapToDto(transactions));

        }).WithTags("Transaction")
        .RequireAuthorization();



}
