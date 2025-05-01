/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using System.Linq.Expressions;

namespace Steve.ManagerHero.UserService.Application.Interfaces.Repository;

public interface IPermissionRepository : IRepository
{
    /// <summary>
    /// Create a new permission.
    /// </summary>
    /// <param name="permission"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Permission> CreateAsync(Permission permission, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get a permission by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Permission?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all permissions.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<(IEnumerable<Permission> items, int totalCount)> GetPermissionsAsync(
        Expression<Func<Permission, bool>>? filter,
        int pageNumber = PaginationConstant.PageNumberDefault,
        int pageSize = PaginationConstant.PageSizeDefault,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Update a permission.
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    bool Update(Permission permission);

    /// <summary>
    /// Delete a permission.
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    bool Delete(Permission permission);

}