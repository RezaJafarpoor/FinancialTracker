using Backend;
using Backend.Shared.Domain;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices(builder.Configuration);
var app = builder.Build();

app.MapGet("/test", () =>
{
});

app.Run();
