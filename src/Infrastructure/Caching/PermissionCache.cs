/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.Extensions.Caching.Memory;
using Steve.ManagerHero.UserService.Application.Interfaces.Caching;

namespace Steve.ManagerHero.UserService.Infrastructure.Caching;

public class PermissionCache(
    IMemoryCache _memoryCache
) : IPermissionCache
{
    private const string CacheKey = "permission:{0}";

    private const string UserPermissionKey = "users:{0}:permissions";

    public void SetSession(Session session)
    {
        _memoryCache.Set(
            string.Format(CacheKey, session.Id),
            session
        );
    }

    public bool GetById(Guid sessionId, out Permission? permission)
    {
        // Try to get the cached user by permissionId
        if (_memoryCache.TryGetValue(string.Format(CacheKey, sessionId), out permission) && permission != null)
        {
            return true; // Permission found in cache
        }

        permission = null; // Permission not found in cache
        return false;
    }

    public void Clear(Guid permissionId)
    {
        // Remove the user from the cache by permissionId
        _memoryCache.Remove(string.Format(CacheKey, permissionId));
    }

    public void ClearByUserId(Guid userId)
    {
        // Remove the user from the cache by userId
        _memoryCache.Remove(string.Format(UserPermissionKey, userId));
    }

    public void SetPermissionsByUserId(Guid userId, string[] permissions)
    {
        _memoryCache.Set(
            string.Format(UserPermissionKey, userId),
            permissions
        );
    }

    public bool GetPermissionsByUserId(Guid userId, out string[]? permissions)
    {
        // Try to get the cached user by userId
        if (_memoryCache.TryGetValue(string.Format(UserPermissionKey, userId), out permissions) && permissions != null)
        {
            return true; // Permissions found in cache
        }

        permissions = null; // Permissions not found in cache
        return false;
    }
}