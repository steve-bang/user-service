
using System.Security.Claims;
using Steve.ManagerHero.UserService.Domain.Exceptions;

namespace Steve.ManagerHero.UserService.Application.Auth;

public interface IIdentityService
{
    /// <summary>
    /// Get current user id from request.
    /// </summary>
    /// <returns>Returns current user id.</returns>
    Guid GetUserIdRequest();

    /// <summary>
    /// Get access token from request
    /// </summary>
    /// <returns>Returns the access token value.</returns>
    string GetAccessTokenRequest();
}

public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserIdRequest()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
        {
            throw new UnauthorizedException();
        }

        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier) ??
                          user.FindFirst("sub"); // Fallback for JWT sub claim

        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            throw new UnauthorizedException("User ID is invalid or missing.");
        }

        return userId;
    }

    public string GetAccessTokenRequest()
    {
        var authorizationHeader = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].FirstOrDefault();

        if (authorizationHeader == null || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            throw new UnauthorizedException("Access token is missing.");
        }

        return authorizationHeader.Substring("Bearer ".Length).Trim();
    }
}