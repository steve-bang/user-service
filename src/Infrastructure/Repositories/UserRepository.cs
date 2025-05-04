/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Steve.ManagerHero.UserService.Domain.ValueObjects;

namespace Steve.ManagerHero.UserService.Infrastructure.Repository;

public class UserRepository(
    UserAppContext _context
) : IUserRepository
{

    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        var userAdded = await _context.Users.AddAsync(user);

        return userAdded.Entity;
    }

    public bool Delete(User user)
    {
        var userDeleted = _context.Users.Remove(user);

        return userDeleted.State == EntityState.Deleted;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _context
            .Users
            .FirstOrDefaultAsync(x => x.EmailAddress == new EmailAddress(email));

        return user;
    }

    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .AsSplitQuery()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<(IEnumerable<User> items, int totalCount)> GetUsersAsync(
        Expression<Func<User, bool>> filter,
        int pageNumber = PaginationConstant.PageNumberDefault,
        int pageSize = PaginationConstant.PageSizeDefault,
        CancellationToken cancellationToken = default
    )
    {
        var query = _context.Users.AsQueryable();

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

    public async Task<(IEnumerable<User> items, int totalCount)> GetUsersByRoleIdAsync(
        Guid roleId,
        int pageNumber = PaginationConstant.PageNumberDefault,
        int pageSize = PaginationConstant.PageSizeDefault,
        CancellationToken cancellationToken = default
    )
    {
        var query = _context.Users.AsQueryable();

        query = query.Include(u => u.UserRoles)
                .Where(
                    u => u.UserRoles.Any(ur => ur.RoleId == roleId)
                ).AsSplitQuery();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(u => u.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsExistEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _context
            .Users
            .FirstOrDefaultAsync(x => x.EmailAddress == new EmailAddress(email));

        return user != null;
    }

    public Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public User Update(User user)
    {
        var result = _context.Users.Update(user);

        return result.Entity;
    }
}