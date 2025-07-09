using Backend.Shared.Domain;
using Backend.Shared.Interfaces;
using Backend.Shared.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.CreateTransaction;

public record CreateTransactionDto(string IncomeType, int Amount, string Description);

public class CreateTransaction : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
         => app.MapGroup("transaction").MapPost("/transaction", ([FromBody] CreateTransactionDto dto, ApplicationContext dbContext) =>
         {
             // Get user Id from token then create the transaction
             //  var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == token);
             //  var transaction = Transaction.CreateTransaction(dto.IncomeType, dto.Amount, dto.Description, user);
             //  save transaction and return
         }).WithTags("Transaction");
}
