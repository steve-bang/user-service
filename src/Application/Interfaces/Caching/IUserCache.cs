/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.Interfaces.Caching;

public interface IUserCache
{
    /// <summary>
    /// Gets the user from the cache by user ID.
    /// If the user is found, it returns true and outputs the user object.
    /// If the user is not found, it returns false and outputs null.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="user">The user object if found; otherwise, null.</param>
    /// <returns>Returns true if the user is found in the cache; otherwise, false.</returns>
    bool GetByUserId(Guid userId, out User? user);

    /// <summary>
    /// Sets the user in the cache.
    /// This method stores the user object in the cache and returns a string identifier for the user.
    /// </summary>
    /// <param name="user">The user object to be cached.</param>
    /// <returns></returns>
    void SetUser(User user);

    /// <summary>
    /// Clears the user from the cache by user ID.
    /// This method removes the user object from the cache based on the provided user ID.
    /// If the user is not found, it does nothing.
    /// </summary>
    void ClearUserById(Guid userId);
}