using Backend.Shared.Interfaces;
using Backend.Shared.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Features.UpdateTransaction;

public record UpdateTransactionDto(Guid Id, string IncomeType, int Amount, string Description);


public class UpdateTrasaction : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
     => app.MapGroup("transaction").MapPut("/transaction", async ([FromBody] UpdateTransactionDto dto, ApplicationContext dbContext) =>
     {

     }).WithTags("Transaction");


}
