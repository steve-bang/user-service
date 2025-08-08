/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.Extensions.Caching.Memory;
using Steve.ManagerHero.UserService.Application.Interfaces.Caching;

namespace Steve.ManagerHero.UserService.Infrastructure.Caching;

public class SessionCache(
    IMemoryCache _memoryCache
) : ISessionCache
{
    private const string CacheKey = "session:{0}";

    public void SetSession(Session session)
    {
        _memoryCache.Set(
            string.Format(CacheKey, session.Id),
            session
        );
    }

    public bool GetById(Guid sessionId, out Session? session)
    {
        // Try to get the cached user by sessionId
        if (_memoryCache.TryGetValue(string.Format(CacheKey, sessionId), out session) && session != null)
        {
            return true; // Session found in cache
        }

        session = null; // Session not found in cache
        return false;
    }

    public void Clear(Guid sessionId)
    {
        // Remove the user from the cache by userId
        _memoryCache.Remove(string.Format(CacheKey, sessionId));
    }
}