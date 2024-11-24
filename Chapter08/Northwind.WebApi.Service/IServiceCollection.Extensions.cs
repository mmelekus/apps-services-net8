using AspNetCoreRateLimit; // To use ClientRateLimitOptions and so on.
using Microsoft.AspNetCore.HttpLogging; // To use HttpLoggingFields.

namespace Northwind.WebApi.Service;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCustomHttpLogging(this IServiceCollection services)
    {
        services.AddHttpLogging(options => {
            // Add the Origin header so it will not be redacted.
            options.RequestHeaders.Add("Origin");
            // Add the rate limiting headers so they will not be redacted.
            options.RequestHeaders.Add("X-Client-Id");
            options.ResponseHeaders.Add("Retry-After");
            // By default, the response body is not included.
            options.LoggingFields = HttpLoggingFields.All;
        });

        return services;
    }

    public static IServiceCollection AddCustomRateLimiting(this IServiceCollection services, ConfigurationManager configuration)
    {
        // Add services to store rate limit counters and rules in memory.
        services.AddMemoryCache();
        services.AddInMemoryRateLimiting();
        // Load default rate limit options from appsettings.json
        services.Configure<ClientRateLimitOptions>(configuration.GetSection("ClientRateLimiting"));
        // Load client-specific policies from appsettings.json
        services.Configure<ClientRateLimitPolicies>(configuration.GetSection("ClientRateLimitPolicies"));
        // Register the configuration
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

        return services;
    }
}
