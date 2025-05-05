/*
* Author: Steve Bang
* History:
* - [2025-05-01] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Permissions.Commands;

public record CreatePermissionCommand(
    string Code,
    string Name,
    string Description
) : IRequest<PermissionDto>;