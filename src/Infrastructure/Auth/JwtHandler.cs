
using System.Security.Claims;
using Steve.ManagerHero.UserService.Application.Auth;
using Steve.ManagerHero.UserService.Domain.AggregatesModel;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Steve.ManagerHero.UserService.Infrastructure.Auth;
public class JwtHandler : IJwtHandler
{
    private readonly JwtSettings _jwtSettings;

    public JwtHandler(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public void GenerateToken(User user, out string accessToken, out string refreshToken, out DateTime expires)
    {
        var claims = new List<Claim>
        {
            new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new (ClaimTypes.NameIdentifier, user.DisplayName),
            new (ClaimTypes.Email, user.EmailAddress.Value),
            new (ClaimTypes.Role, string.Join(",", user.RoleNames))
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256
        );

        expires = DateTime.Now.AddHours(_jwtSettings.AccessTokenExpiryHours);

        // Generate the tokens
        accessToken = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            claims: claims,
            expires: expires,
            audience: _jwtSettings.Audience,
            signingCredentials: signingCredentials
        ));

        // Generate the refresh token
        refreshToken = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            claims: claims,
            expires: DateTime.Now.AddHours(_jwtSettings.RefreshTokenExpiryHours),
            audience: _jwtSettings.Audience,
            signingCredentials: signingCredentials
        ));
    }

    public IEnumerable<Claim> ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                ValidateLifetime = true
            }, out var validatedToken);

            return ((JwtSecurityToken)validatedToken).Claims;
        }
        catch (Exception ex)
        {
            // log the exception
            Console.WriteLine(ex.Message);
            return Enumerable.Empty<Claim>();
        }
    }
}