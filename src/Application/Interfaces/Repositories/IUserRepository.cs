/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using System.Linq.Expressions;
using Steve.ManagerHero.UserService.Domain.Common;

namespace Steve.ManagerHero.UserService.Application.Interfaces.Repository;

public interface IUserRepository : IRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<bool> IsExistEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken = default);

    Task<(IEnumerable<User> items, int totalCount)> GetUsersAsync(
        Expression<Func<User, bool>>? filter,
        int pageNumber = PaginationConstant.PageNumberDefault,
        int pageSize = PaginationConstant.PageSizeDefault,
        CancellationToken cancellationToken = default
    );

    Task<(IEnumerable<User> items, int totalCount)> GetUsersByRoleIdAsync(
        Guid roleId,
        int pageNumber = PaginationConstant.PageNumberDefault,
        int pageSize = PaginationConstant.PageSizeDefault,
        CancellationToken cancellationToken = default
    );

    User Update(User user);
    bool Delete(User user);
}