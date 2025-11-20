/*
* Author: Steve Bang
* Description: Middleware to restrict access to the API based on the IP address
* History:
* - [2025-05-10] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.BuildingBlocks.Utilities;

namespace Steve.ManagerHero.Middlewares;

public class IpRestrictionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<IpRestrictionMiddleware> _logger;

    private readonly List<string> _blacklistIps;

    public IpRestrictionMiddleware(
        RequestDelegate next,
        ILogger<IpRestrictionMiddleware> logger,
        IConfiguration configuration
    )
    {
        _next = next;
        _logger = logger;

        // You can load this list from appsettings.json or database
        _blacklistIps = configuration.GetSection("BlacklistIPs").Get<List<string>>() ?? new();

        // Validate IP formats
        ValidateBlacklistIPs();
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var remoteIp = GetRemoteIpAddress(httpContext);

        if (!string.IsNullOrEmpty(remoteIp) && IsIpBlacklisted(remoteIp)
        )
        {
            _logger.LogWarning("Access denied for blacklisted IP: {IP}", remoteIp);

            throw new IpRestrictException();
        }

        await _next(httpContext);
    }

    private string? GetRemoteIpAddress(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();

        // Check for forwarded headers (in case behind proxy/load balancer)
        if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].ToString();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                // Take the first IP in the chain (the original client)
                ipAddress = forwardedFor.Split(',')[0].Trim();
            }
        }

        return ipAddress;
    }

    private bool IsIpBlacklisted(string ipAddress)
    {
        if (string.IsNullOrEmpty(ipAddress))
            return false;

        foreach (var blacklistIP in _blacklistIps)
        {
            if (IpAddressHelper.IsIpInRange(ipAddress, blacklistIP))
            {
                return true;
            }
        }

        return false;
    }

    private void ValidateBlacklistIPs()
    {
        var invalidIPs = _blacklistIps.Where(ip => !IpAddressHelper.IsValidCIDR(ip)).ToList();

        if (invalidIPs.Any())
        {
            _logger.LogWarning("Invalid IP formats in blacklist: {InvalidIPs}", string.Join(", ", invalidIPs));
            // Remove invalid IPs or throw exception based on your requirements
            // _blacklistIPs = _blacklistIPs.Except(invalidIPs).ToList();
        }
    }
}