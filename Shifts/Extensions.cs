using System.Security.Claims;
using System.Threading.RateLimiting;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Shifts;

public static class Extensions
{
    private const string Policy = "PerUserRatelimit";

    public static TBuilder AddServiceDefaults<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.AddDefaultHealthChecks();

        return builder;
    }

    public static WebApplication MapDefaultEndpoints(this WebApplication app)
    {
        // Adding health checks endpoints to applications in non-development environments has security implications.
        // See https://aka.ms/dotnet/aspire/healthchecks for details before enabling these endpoints in non-development environments.
        if (app.Environment.IsDevelopment())
        {
            // All health checks must pass for app to be considered ready to accept traffic after starting
            app.MapHealthChecks("/health");

            // Only health checks tagged with the "live" tag must pass for app to be considered alive
            app.MapHealthChecks("/alive", new HealthCheckOptions
            {
                Predicate = r => r.Tags.Contains("live")
            });
        }

        return app;
    }

    private static TBuilder AddDefaultHealthChecks<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
    {
        builder.Services.AddHealthChecks()
            // Add a default liveness check to ensure app is responsive
            .AddCheck("self", () => HealthCheckResult.Healthy(), ["live"]);

        return builder;
    }



    public static IServiceCollection AddRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter();

        services.AddOptions<TokenBucketRateLimiterOptions>()
            .Configure(options =>
            {
                options.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
                options.AutoReplenishment = true;
                options.TokenLimit = 100;
                options.TokensPerPeriod = 100;
                options.QueueLimit = 100;
            }).BindConfiguration("RateLimiting");

        services.AddOptions<RateLimiterOptions>()
            .Configure((RateLimiterOptions options,
                IOptionsMonitor<TokenBucketRateLimiterOptions> perUserRateLimiterOptions) =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddPolicy(Policy, context =>
                {
                    var username = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    return RateLimitPartition.GetTokenBucketLimiter(username, _ => perUserRateLimiterOptions.CurrentValue);
                });
            });

        return services;
    }

    public static IEndpointConventionBuilder RequirePerUserRateLimit(this IEndpointConventionBuilder builder)
    {
        return builder.RequireRateLimiting(Policy);
    }
}