using System.Security.Claims;
using System.Threading.RateLimiting;

using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Options;

namespace Shifts.Extensions
{
    public static class RateLimitingExtensions
    {
        private const string Policy = "PerUserRatelimit";

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
                        return RateLimitPartition.GetTokenBucketLimiter(username,
                            _ => perUserRateLimiterOptions.CurrentValue);
                    });
                });

            return services;
        }

        public static IEndpointConventionBuilder RequirePerUserRateLimit(this IEndpointConventionBuilder builder)
        {
            return builder.RequireRateLimiting(Policy);
        }
    }
}