/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/



using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Steve.ManagerHero.UserService.Infrastructure.Repository;

public class PermissionRepository(
    UserAppContext _context
) : IPermissionRepository
{
    public async Task<Permission> CreateAsync(Permission permission, CancellationToken cancellationToken = default)
    {
        var roleCreated = await _context.Permissions.AddAsync(permission, cancellationToken);

        return roleCreated.Entity;
    }

    public Task<Permission?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Permissions.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<(IEnumerable<Permission> items, int totalCount)> GetPermissionsAsync(
        Expression<Func<Permission, bool>> filter,
        int pageNumber = PaginationConstant.PageNumberDefault,
        int pageSize = PaginationConstant.PageSizeDefault,
        CancellationToken cancellationToken = default
    )
    {
        var query = _context.Permissions.AsQueryable();

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

    public bool Update(Permission permission)
    {
        var permissionToUpdate = _context.Permissions.Update(permission);

        return permissionToUpdate.State == EntityState.Modified;
    }

    public bool Delete(Permission permission)
    {
        var permissionToDelete = _context.Permissions.Remove(permission);

        return permissionToDelete.State == EntityState.Deleted;
    }
}