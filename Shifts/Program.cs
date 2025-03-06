using Microsoft.AspNetCore.HttpLogging;

using Shifts;
using Shifts.Extensions;
using Shifts.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<DataSeeder>();

var connectionString = builder.Configuration.GetConnectionString("DbConnectionString") ?? "Data Source=.db/Shifts.db";
builder.Services.AddScoped<IDataRepository, DataRepository>();
builder.Services.AddSqlite<ShiftsDbContext>(connectionString);

// Configure Rate Limiting
builder.Services.AddRateLimiting();

// Configure Documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// Configure Logging and Monitoring
builder.Services.AddHealthChecks();
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
app.UseHealthChecks("/health");
app.UseRateLimiter();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi("/spec.json");
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

// Setup application endpoints
app.MapShiftsApi();

if (args.Length == 1 && args[0].Equals("seed", StringComparison.CurrentCultureIgnoreCase))
{
    app.SeedData();
    return;
}

app.Run();
