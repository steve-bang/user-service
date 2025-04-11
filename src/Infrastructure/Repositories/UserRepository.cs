/*
* Author: Steve Bang
* History:
* - [2024-04-11] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Application.Interfaces.Repository;
using Steve.ManagerHero.UserService.Domain.AggregatesModel;
using Steve.ManagerHero.UserService.Domain.Common;

namespace Steve.ManagerHero.UserService.Infrastructure.Repository;

public class UserRepository(
    UserAppContext _context
) : IUserRepository
{
    public IUnitOfWork UnitOfWork => _context;

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
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

    public Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> SearchUsersAsync(string searchTerm, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}