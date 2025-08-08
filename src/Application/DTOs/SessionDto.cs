/*
* Author: Steve Bang
* History:
* - [2025-08-03] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.UserService.Application.DTOs;

public class SessionDto
{
    public Guid Id { get; set; }
    public string RefreshToken { get; set; } = null!;
    public string IpAddress { get; set; } = null!;
    public string UserAgent { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime? RevokedAt { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    public SessionDto() { }
}
