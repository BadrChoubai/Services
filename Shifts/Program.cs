using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;

using Shifts;
using Shifts.Model;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddTransient<DataSeeder>();

var connectionString = builder.Configuration.GetConnectionString("DbConnectionString") ?? "Data Source=.db/Shifts.db";
builder.Services.AddSqlite<ShiftsDbContext>(connectionString);

// Configure Open API
builder.Services.AddOpenApi();
builder.Services.AddRateLimiting();

builder.Services.AddHttpLogging(o =>
{
    if (builder.Environment.IsDevelopment())
    {
        o.CombineLogs = true;
        o.LoggingFields = HttpLoggingFields.ResponseBody | HttpLoggingFields.ResponseHeaders;
    }
});

var app = builder.Build();

app.UseHttpLogging();
app.UseRateLimiter();

app.MapOpenApi();
app.MapDefaultEndpoints();

if (args.Length == 1 && args[0].Equals("seed", StringComparison.CurrentCultureIgnoreCase))
    SeedData(app);


app.Map("/", () => Results.Redirect("/openapi/v1.json"));

Endpoints.MapShiftsApi(app);

app.Run();
return;

//Seed Data
static void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using var scope = scopedFactory?.CreateScope();
    var service = scope?.ServiceProvider.GetService<DataSeeder>();
    service?.Seed();
}

