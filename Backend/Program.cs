using Backend;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServices(builder.Configuration);
builder.Services.AddOpenApi();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
}
app.MapOpenApi();
app.RegisterEndpoints();
app.Run();
