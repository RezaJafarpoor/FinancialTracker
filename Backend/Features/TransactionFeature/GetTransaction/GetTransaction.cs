using System.Security.Claims;
using Backend.Features.GetTransaction;
using Backend.Shared;
using Backend.Shared.Domain;
using Backend.Shared.Interfaces;
using Backend.Shared.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.TransactionFeature.GetTransaction;

public record UpdateTransactionDto(Guid Id, string? IncomeType, int? Amount, string? Description);

public class GetTransaction : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
        => app.MapGroup("api")
        .MapGet("transaction/{id:guid}", async ([FromRoute] Guid Id, HttpContext context, ApplicationContext dbContext, TransactionMapper mapper) =>
        {
            var token = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Guid.TryParse(token, out Guid userId);
            var transaction = await dbContext.Transactions
            .FirstOrDefaultAsync(t => t.Id == Id & t.UserId == userId);
            return transaction is null
            ? Results.NotFound()
            : Results.Ok(mapper.MapToDto(transaction));

        }).WithTags("Transaction")
        .RequireAuthorization();


}
