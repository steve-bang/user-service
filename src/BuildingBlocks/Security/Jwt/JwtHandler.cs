
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Steve.ManagerHero.BuildingBlocks.Authentication;

namespace Steve.ManagerHero.BuildingBlocks.Security.Jwt;

public class JwtHandler : IJwtHandler
{

    private readonly JwtOptions _jwtOptions;

    public JwtHandler(JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions;
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


    public (string accessToken, string refreshToken, DateTime expires) GenerateToken(Guid userId, Guid sessionId)
    {
        var claims = new List<Claim>
        {
            new (JwtClaimKeys.UserId, userId.ToString()),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
            new (JwtClaimKeys.SessionId, sessionId.ToString()),
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret)),
            SecurityAlgorithms.HmacSha256
        );

        DateTime expires = DateTime.Now.AddHours(_jwtOptions.AccessTokenExpiryHours);

        // Generate the tokens
        string accessToken = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            claims: claims,
            expires: expires,
            audience: _jwtOptions.Audience,
            signingCredentials: signingCredentials
        ));

        // Generate the refresh token
        string refreshToken = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            claims: claims,
            expires: DateTime.Now.AddHours(_jwtOptions.RefreshTokenExpiryHours),
            audience: _jwtOptions.Audience,
            signingCredentials: signingCredentials
        ));

        return (accessToken, refreshToken, expires);
    }

    public IEnumerable<Claim> ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
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