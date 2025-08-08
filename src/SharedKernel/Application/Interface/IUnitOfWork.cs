/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Infrastructure;

namespace Steve.ManagerHero.SharedKernel.Application.Interface;

/// <summary>
/// Represents a unit of work.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    UserAppContext Context { get; }
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    ISessionRepository Sessions { get; }
    IPermissionRepository Permissions { get; }
    IIdentityRepository Identities { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

}