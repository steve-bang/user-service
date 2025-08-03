
using System.Security.Claims;
using Steve.ManagerHero.UserService.Application.Auth;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Steve.ManagerHero.BuildingBlocks.Authentication;

namespace Steve.ManagerHero.UserService.Infrastructure.Auth;

public class JwtHandler : IJwtHandler
{

    private readonly JwtSettings _jwtSettings;

    public JwtHandler(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    public Guid ExtraSessionId(string accessToken)
    {
        string sessionIdClaim = ExtraByKey(accessToken, JwtClaimKeys.SessionId);

        if (!Guid.TryParse(sessionIdClaim, out var sessionId))
        {
            throw new SecurityTokenException("Invalid SessionId format in token.");
        }

        return sessionId;
    }

    public Guid ExtraUserId(string accessToken)
    {
        string userIdClaim = ExtraByKey(accessToken, JwtClaimKeys.UserId);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            throw new SecurityTokenException("Invalid UserId format in token.");
        }

        return userId;
    }

    public string ExtraByKey(string accessToken, string key)
    {
        var claims = ValidateToken(accessToken);

        var keyClaim = claims.FirstOrDefault(c => c.Type == key);
        if (keyClaim == null)
        {
            throw new SecurityTokenException($"{key} claim not found in the token.");
        }

        return keyClaim.Value;
    }


    public void GenerateToken(User user, Session session, out string accessToken, out string refreshToken, out DateTime expires)
    {
        var claims = new List<Claim>
        {
            new (JwtClaimKeys.UserId, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new (JwtClaimKeys.SessionId, session.Id.ToString()),
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