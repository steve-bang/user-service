/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.Interfaces.Repository;

public interface ISessionRepository : IRepository
{
    Task<Session> CreateAsync(Session session, CancellationToken cancellationToken = default);

    Task<List<Session>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Session?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Update(Session session);

    bool Delete(Session session);
}