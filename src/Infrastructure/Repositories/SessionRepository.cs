/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/


/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.EntityFrameworkCore;
using Steve.ManagerHero.UserService.Domain.Common;

namespace Steve.ManagerHero.UserService.Infrastructure.Repository;

public class SessionRepository(
    UserAppContext _context
) : ISessionRepository
{

    public async Task<Session> CreateAsync(Session session, CancellationToken cancellationToken = default)
    {
        var result = await _context.Sessions.AddAsync(session);

        return result.Entity;
    }

    public bool Delete(Session session)
    {
        var result = _context.Sessions.Remove(session);

        return result.State == Microsoft.EntityFrameworkCore.EntityState.Deleted;
    }

    public Task<Session?> GetByAccessTokenAsync(string accessToken, CancellationToken cancellationToken = default)
    {
        return _context.Sessions.FirstOrDefaultAsync(s => s.AccessToken == accessToken);
    }

    public Task<List<Session>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return _context.Sessions.Where(s => s.UserId == userId).ToListAsync();
    }

    public void Update(Session session)
    {
        _context.Sessions.Update(session);
    }
}