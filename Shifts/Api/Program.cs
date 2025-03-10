using Shifts;
using Shifts.Api;
using Shifts.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddServices();
builder.Services.AddDatabase(config);
builder.Services.AddMiddleware(
    builder.Environment.IsDevelopment()
);

// Configure Documentation
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDocumentation();
}

var app = builder.Build();
app.UseMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseDocumentation();
}

// Setup application endpoints
Endpoints.MapShiftsApi(app);

if (args.Length == 1 && args[0].Equals("seed", StringComparison.CurrentCultureIgnoreCase))
{
    app.SeedData();
    return;
}

app.Run();