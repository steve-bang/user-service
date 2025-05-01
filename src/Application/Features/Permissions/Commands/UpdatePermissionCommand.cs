/*
* Author: Steve Bang
* History:
* - [2025-05-01] - Updated by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Permissions.Commands;

public record UpdatePermissionCommand(
    Guid Id,
    string Code,
    string Name,
    string Description
) : IRequest<PermissionDto>;