/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

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
        return _context.Roles
                .Include(r => r.RolePermissions)
                .ThenInclude(rp => rp.Permission)
                .AsSplitQuery()
                .FirstOrDefaultAsync(r => r.Id == id);
    }

    public Task<List<Role>> GetRolesByPermissionId(Guid permissionId, CancellationToken cancellationToken = default)
    {
        return _context.Roles
            .Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .AsSplitQuery()
            .Where(r => r.RolePermissions.Any(rp => rp.PermissionId == permissionId))
            .ToListAsync();
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

    public async Task<(IEnumerable<Role> items, int totalCount)> GetRolesAsync(
        Expression<Func<Role, bool>> filter,
        int pageNumber = PaginationConstant.PageNumberDefault,
        int pageSize = PaginationConstant.PageSizeDefault,
        CancellationToken cancellationToken = default
    )
    {
        var query = _context.Roles.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(u => u.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public Role Update(Role role, CancellationToken cancellationToken = default)
    {
        var roleUpdated = _context.Roles.Update(role);

        return roleUpdated.Entity;
    }

    public async Task<IEnumerable<Role>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Roles
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }
}