/*
* Author: Steve Bang
* History:
* - [2025-05-04] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.DTO;

/// <summary>
/// Request object for remove roles from a user
/// </summary>
/// <param name="PermissionIds"></param>
public record RemoveRolesFromUserRequest(
    Guid[] RoleIds // The IDs of the roles to remove from a user
);
