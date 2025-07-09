using Backend.Shared.Interfaces;
using Backend.Shared.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Features.DeleteTransaction;



public class DeleteTransaction : IEndpoint
{
    public void Register(IEndpointRouteBuilder app)
        => app.MapGroup("transaction").MapDelete("/transaction", async ([FromBody] Guid id, ApplicationContext dbContext) =>
     {

     }).WithTags("Transaction");
}
