/*
* Author: Steve Bang
* History:
* - [2025-05-02] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Permissions.Commands;

public record AssignPermissionToRoleCommand(
    Guid RoleId,
    Guid[] PermissionIds
) : IRequest<bool>;