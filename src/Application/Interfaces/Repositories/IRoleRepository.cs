/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Common;

namespace Steve.ManagerHero.UserService.Application.Interfaces.Repository;

public interface IRoleRepository : IRepository
{
    Task<Role> CreateAsync(Role role, CancellationToken cancellationToken = default);

    Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Role Update(Role role, CancellationToken cancellationToken = default);

    bool Delete(Role role, CancellationToken cancellationToken = default);

    Task<List<Role>> GetRolesByUserId(Guid userId, CancellationToken cancellationToken = default);

    Task<List<Role>> GetRolesByPermissionId(Guid permissionId, CancellationToken cancellationToken = default);
}