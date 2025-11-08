
namespace Steve.ManagerHero.BuildingBlocks.Security.Jwt;

public class JwtOptions
{
    public string Issuer { get; set; } = null!;

    public string Audience { get; set; } = null!;

    public string Secret { get; set; } = null!;

    public int AccessTokenExpiryHours { get; set; }

    public int RefreshTokenExpiryHours { get; set; }
}