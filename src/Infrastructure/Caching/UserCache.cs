/*
* Author: Steve Bang
* History:
* - [2025-05-31] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.Extensions.Caching.Memory;
using Steve.ManagerHero.UserService.Application.Interfaces.Caching;

namespace Steve.ManagerHero.UserService.Infrastructure.Caching;

public class UserCache(
    IMemoryCache _memoryCache
) : IUserCache
{
    private const string CacheKey = "users:{0}";
    private const int TimeToLiveDefaultMinutes = 60; // Default time to live in minutes

    public void ClearUserById(Guid userId)
    {
        // Remove the user from the cache by userId
        _memoryCache.Remove(string.Format(CacheKey, userId));
    }

    public bool GetByUserId(Guid userId, out User? user)
    {
        // Try to get the cached user by userId
        if (_memoryCache.TryGetValue(string.Format(CacheKey, userId), out user) && user != null)
        {
            return true; // User found in cache
        }

        user = null; // User not found in cache
        return false;
    }

    public void SetUser(User user)
    {
        _memoryCache.Set(
            string.Format(CacheKey, user.Id),
            user,
            new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(TimeToLiveDefaultMinutes))
        );
    }
}