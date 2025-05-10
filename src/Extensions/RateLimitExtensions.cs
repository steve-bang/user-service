/*
* Author: Steve Bang
* History:
* - [2025-05-10] - Created by mrsteve.bang@gmail.com
*/

using System.Threading.RateLimiting;

namespace Steve.ManagerHero.UserService.Extensions;

public static class RateLimitExtensions
{
    public static IHostApplicationBuilder AddRateLimitSettings(this IHostApplicationBuilder builder)
    {
        builder.Services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            {
                // Defind path for limit
                string path = httpContext.Request.Path.ToString();

                // Different limits for different paths

                // Config with register a new user
                if (path.StartsWith("/api/v1/auth/register"))
                {
                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: $"{httpContext.Connection.RemoteIpAddress}-{path}",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 2,
                            Window = TimeSpan.FromMinutes(1)
                        });
                }

                // Config with forgot password
                if (path.StartsWith("/api/v1/auth/forgot-password"))
                {
                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: $"{httpContext.Connection.RemoteIpAddress}-{path}",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 2,
                            Window = TimeSpan.FromMinutes(1)
                        });
                }

                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.User.Identity?.Name ?? httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 3, // Number of requests per window
                        QueueLimit = 0, // Number of requests that can be queued
                        Window = TimeSpan.FromMinutes(1) // Time window for the rate limit
                    }
                );
            }
        );

            options.OnRejected = (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                // Add reset time is 1 minute
                context.HttpContext.Response.Headers["X-RateLimit-Reset"] = DateTime.Now.AddMinutes(1).ToString();

                // Optional logging
                Console.WriteLine($"Rate limit exceeded for IP: {context.HttpContext.Connection.RemoteIpAddress}");
                throw new RateLimitingException();
            };
        });

        return builder;
    }
}