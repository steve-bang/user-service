
namespace Steve.ManagerHero.UserService.Infrastructure.Auth;

public class JwtSettings
{
    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public int AccessTokenExpiryHours { get; set; }

    public int RefreshTokenExpiryHours { get; set; }
}