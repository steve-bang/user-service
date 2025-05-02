/*
* Author: Steve Bang
* History:
* - [2025-05-02] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.DTO;

/// <summary>
/// Request object for assigning permissions to a role.
/// </summary>
/// <param name="PermissionIds"></param>
public record AssignPermissionsToRoleRequest(
    Guid[] PermissionIds // The IDs of the permissions to assign to the role
);
