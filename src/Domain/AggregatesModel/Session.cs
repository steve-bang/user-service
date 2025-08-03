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
    public string RefreshToken { get; private set; }
    public string IpAddress { get; private set; }
    public string UserAgent { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public bool IsRevoked { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Session() : base() { }

    public Session(
        User user,
        string refreshToken,
        string ipAddress,
        string userAgent,
        DateTime expiresAt
    ) : this(user)
    {
        RefreshToken = refreshToken;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        ExpiresAt = expiresAt.ToUniversalTime();
    }

    public Session(
        User user
    ) : this()
    {
        UserId = user.Id;
        User = user;
        IsActive = true;
        IsRevoked = false;
    }

    public void Update(
        string refreshToken,
        HttpContext httpContext,
        DateTime expiresAt
    )
    {
        string userAgent = httpContext.Request.Headers.UserAgent.ToString() ?? string.Empty;
        string ipAddress = httpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

        RefreshToken = refreshToken;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        ExpiresAt = expiresAt.ToUniversalTime();
    }



    public void Deactivate()
    {
        IsActive = false;
    }

    public void Revoked()
    {
        IsActive = false;
        IsRevoked = true;
        RevokedAt = DateTime.UtcNow;
    }
}