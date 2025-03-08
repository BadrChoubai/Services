using Microsoft.AspNetCore.HttpLogging;

using Shifts;
using Shifts.Extensions;
using Shifts.Repository;
using Shifts.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<DataSeeder>();

// Configure Database
var connectionString = builder.Configuration.GetConnectionString("DbConnectionString") ?? "Data Source=.db/Shifts.db";
builder.Services.AddScoped<IDataRepository, DataRepository>();
builder.Services.AddSqlite<ShiftsDbContext>(connectionString);

// Create Service
builder.Services.AddScoped<IApiService, ShiftsService>();

// Configure Rate Limiting
builder.Services.AddRateLimiting();

// Configure Documentation
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddOpenApi();
}

// Configure Logging and Monitoring
builder.Services.AddHealthChecks();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.Services.AddHttpLogging(o =>
{
    if (builder.Environment.IsDevelopment())
    {
        o.CombineLogs = true;
        o.LoggingFields = builder.Environment.IsDevelopment()
            ? HttpLoggingFields.ResponseBody | HttpLoggingFields.ResponseHeaders
            : HttpLoggingFields.RequestProperties | HttpLoggingFields.ResponsePropertiesAndHeaders;
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
Endpoints.MapShiftsApi(app);

if (args.Length == 1 && args[0].Equals("seed", StringComparison.CurrentCultureIgnoreCase))
{
    app.SeedData();
    return;
}

app.Run();
