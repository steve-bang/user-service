/*
* Author: Steve Bang
* History:
* - [2025-08-03] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.Interfaces.Caching;

public interface ISessionCache
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    void SetSession(Session session);

    /// <summary>
    /// Gets the session from the cache by session ID.
    /// If the session is found, it returns true and outputs the session object.
    /// If the session is not found, it returns false and outputs null.
    /// </summary>
    /// <param name="sessionId">The unique identifier of the session.</param>
    /// <param name="session">The session object if found; otherwise, null.</param>
    /// <returns>Returns true if the session is found in the cache; otherwise, false.</returns>
    bool GetById(Guid sessionId, out Session? session);

    void Clear(Guid sessionId);

}