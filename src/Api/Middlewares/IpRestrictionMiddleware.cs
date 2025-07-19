/*
* Author: Steve Bang
* Description: Middleware to restrict access to the API based on the IP address
* History:
* - [2025-05-10] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Middlewares;

public class IpRestrictionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<IpRestrictionMiddleware> _logger;

    private readonly List<string> _allowedIps;

    public IpRestrictionMiddleware(
        RequestDelegate next,
        ILogger<IpRestrictionMiddleware> logger,
        IConfiguration configuration
    )
    {
        _next = next;
        _logger = logger;

        // You can load this list from appsettings.json or database
        _allowedIps = configuration.GetSection("AllowedIPs").Get<List<string>>() ?? new();
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var remoteIp = httpContext.Connection.RemoteIpAddress?.ToString();

        if (_allowedIps.Any()
            && !string.IsNullOrEmpty(remoteIp)
            && !_allowedIps.Contains(remoteIp)
        )
        {
            _logger.LogWarning("IP access not allowed: {Ip}", remoteIp);
            throw new IpRestrictException();
        }

        await _next(httpContext);
    }
}