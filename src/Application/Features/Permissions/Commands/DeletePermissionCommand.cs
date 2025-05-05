/*
* Author: Steve Bang
* History:
* - [2025-05-01] - Updated by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Permissions.Commands;

public record DeletePermissionCommand(
    Guid Id
) : IRequest<bool>;