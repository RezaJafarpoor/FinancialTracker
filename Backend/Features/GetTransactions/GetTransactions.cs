using System.Security.Claims;
using Backend.Shared;
using Backend.Shared.Domain;
using Backend.Shared.Interfaces;
using Backend.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.GetTransaction;

public record TransactionDto(Guid Id, string IncomeType, int Amount, string DateTime, string Description);
public class GetTransactions(DateTimeConverter timeConverter) : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
        => app.MapGroup("api")
        .MapGet("transaction", async (HttpContext context, ApplicationContext dbContext) =>
        {
            var token = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(token, out Guid userId);
            var transactions = await dbContext.Transactions.Where(t => t.UserId == userId).ToListAsync();
            return transactions is null
            ? Results.NotFound()
            : Results.Ok(MapToDto(transactions));

        }).WithTags("Transaction")
        .RequireAuthorization();


    private List<TransactionDto> MapToDto(List<Transaction> transactions)
    {
        var list = new List<TransactionDto>();
        foreach (var transaction in transactions)
        {
            var dto = new TransactionDto
            (
               Id: transaction.Id,
                IncomeType: transaction.IncomeType,
                Amount: transaction.Amount,
                DateTime: timeConverter.ConvertToPersianCalender(transaction.DateTime),
                Description: transaction.Description
            );
            list.Add(dto);
        }
        return list;
    }
}
