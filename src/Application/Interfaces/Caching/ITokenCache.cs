/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.Interfaces.Caching;

public interface ITokenCache
{
    /// <summary>
    /// Sets a token in the cache with a specified time to live.
    /// This method stores the token in the cache and returns a string identifier for the token.
    /// </summary>
    /// <param name="token">The token to be cached.</param>
    /// <param name="timeToLiveMinutes">The time to live for the token in minutes.</param>
    /// <returns>Returns a string identifier for the cached token.</returns>
    string SetToken(string token, int timeToLiveMinutes);

    /// <summary>
    /// Checks if the specified token exists in the cache.
    /// This method returns true if the token is found in the cache, otherwise false.
    /// </summary>
    /// <param name="token">The token to check for existence in the cache.</param>
    /// <returns></returns>
    bool IsExistsToken(string token);

    /// <summary>
    /// Revokes the specified token.
    /// This method removes the token from the cache, effectively invalidating it.
    /// </summary>
    /// <param name="token"></param>
    void RevokeToken(string token);
}