using System.Security.Claims;

namespace Steve.ManagerHero.BuildingBlocks.Security.Jwt;

public interface IJwtHandler
{
    /// <summary>
    /// Generates a JWT token for a user.
    /// </summary>
    /// <param name="userId">The id of the user to generate the token for.</param>
    /// <param name="sessionId">The id of the session generated access token.</param>
    (string accessToken, string refreshToken, DateTime expires) GenerateToken(Guid userId, Guid sessionId);

    /// <summary>
    /// Validates a JWT token.
    /// </summary>
    /// <param name="token">The token to validate.</param>
    /// <returns>The claims from the token.</returns>
    IEnumerable<Claim> ValidateToken(string token);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="accessToken"></param>
    /// <returns></returns>
    Guid ExtraSessionId(string accessToken);

    Guid ExtraUserId(string accessToken);
}