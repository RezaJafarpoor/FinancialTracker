using Backend;
using Backend.Shared;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<CalenderHelper>();
var app = builder.Build();

app.MapGet("/test", (CalenderHelper service) =>
{
    var date = service.ConvertToPersian();
    return Results.Ok(date);

});

app.Run();
