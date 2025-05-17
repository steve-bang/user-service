/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.AggregatesModel;

public class Session : AggregateRoot
{
    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public string AccessToken { get; private set; }
    public string RefreshToken { get; private set; }
    public string IpAddress { get; private set; }
    public string UserAgent { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Session() { }

    public Session(
        User user,
        string accessToken,
        string refreshToken,
        string ipAddress,
        string userAgent,
        DateTime expiresAt
    )
    {
        UserId = user.Id;
        User = user;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        ExpiresAt = expiresAt.ToUniversalTime();
        IsActive = true;
    }

    public static Session Create(
        User user,
        string accessToken,
        string refreshToken,
        HttpContext httpContext,
        DateTime expiresAt
    )
    {
        string userAgent = httpContext.Request.Headers.UserAgent.ToString() ?? string.Empty;
        string ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

        return new(user, accessToken, refreshToken, ipAddress, userAgent, expiresAt);
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}