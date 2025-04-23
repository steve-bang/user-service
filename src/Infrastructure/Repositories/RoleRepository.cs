/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.EntityFrameworkCore;
using Steve.ManagerHero.UserService.Domain.Common;

namespace Steve.ManagerHero.UserService.Infrastructure.Repository;

public class RoleRepository(
    UserAppContext _context
) : IRoleRepository
{

    public async Task<Role> CreateAsync(Role role, CancellationToken cancellationToken = default)
    {
        var roleCreated = await _context.Roles.AddAsync(role);

        return roleCreated.Entity;
    }

    public bool Delete(Role role, CancellationToken cancellationToken = default)
    {
        var result = _context.Roles.Remove(role);

        return result.State == EntityState.Deleted;
    }

    public Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Roles.FirstOrDefaultAsync(r => r.Id == id);
    }

    public Task<List<Role>> GetRolesByUserId(Guid userId, CancellationToken cancellationToken = default)
    {
        return _context.Roles
            .Include(r => r.UserRoles)
            .ThenInclude(ur => ur.User)
            .AsSplitQuery()
            .Where(r => r.UserRoles.Any(ur => ur.UserId == userId))
            .ToListAsync();
    }

    public Role Update(Role role, CancellationToken cancellationToken = default)
    {
        var roleUpdated = _context.Roles.Update(role);

        return roleUpdated.Entity;
    }
}