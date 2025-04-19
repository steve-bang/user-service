/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.EntityFrameworkCore;
using Steve.ManagerHero.UserService.Domain.Common;
using Steve.ManagerHero.UserService.Domain.ValueObjects;

namespace Steve.ManagerHero.UserService.Infrastructure.Repository;

public class UserRepository(
    UserAppContext _context
) : IUserRepository
{
    public IUnitOfWork UnitOfWork => _context;

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
        return _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetUsersByRoleAsync(string roleName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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

    public Task<List<User>> SearchUsersAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public User Update(User user)
    {
        var result = _context.Users.Update(user);

        return result.Entity;
    }
}