/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.Extensions.Caching.Memory;
using Steve.ManagerHero.UserService.Application.Interfaces.Caching;

namespace Steve.ManagerHero.UserService.Infrastructure.Caching;

public class TokenCache(
    IMemoryCache _memoryCache
) : ITokenCache
{
    private const string CacheKey = "token:{0}";

    public string SetToken(string token, int timeToLiveMinutes)
    {
        _memoryCache.Set(
            string.Format(CacheKey, token),
            token,
            new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(timeToLiveMinutes))
        );

        return token;
    }

    public bool IsExistsToken(string token)
    {
        // Try to get the cached value
        return _memoryCache.TryGetValue(string.Format(CacheKey, token), out var cachedValue) && cachedValue != null;
    }

    public void RevokeToken(string token)
    {
        _memoryCache.Remove(string.Format(CacheKey, token));
    }
}