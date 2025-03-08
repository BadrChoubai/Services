using Asp.Versioning;

using Microsoft.EntityFrameworkCore;

using Shifts.Repository;
using Shifts.Service;

namespace Shifts.Extensions;

/// <summary>
/// class <c>AddApplicationServices</c> configures core application dependencies
/// </summary>
public static class ApplicationServices
{
    public static void AddServices(
        this IServiceCollection services
    )
    {
        services.AddEndpointsApiExplorer();
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        // Register Services
        services.AddScoped<IDataRepository, DataRepository>();
        services.AddScoped<IApiService, ShiftsService>();
        services.AddTransient<DataSeeder>();
    }

    public static void AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnectionString") ?? "Data Source=.db/Shifts.db";
        services.AddSqlite<ShiftsDbContext>(connectionString);
    }
}