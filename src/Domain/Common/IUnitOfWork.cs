/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.Common;

/// <summary>
/// Represents a unit of work.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    ISessionRepository Sessions { get; }
    IPermissionRepository Permissions { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}