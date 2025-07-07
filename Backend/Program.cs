using Backend;
using Backend.Shared;
using Backend.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices(builder.Configuration);
var app = builder.Build();

app.MapGet("/test", async (AuthService service) =>
{
    var userName = "reza";
    var email = "Reza@gmail.com";
    var password = "RezaPassword1!";
    await service.CreateUser(userName, email, password);
    return Results.Ok();
});
app.MapGet("/user", async (ApplicationContext dbContext) =>
{
    var user = await dbContext.Users.ToListAsync();
    return Results.Ok(user);
});

app.Run();
