/*
* Author: Steve Bang
* History:
* - [2025-04-22] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Roles.Commands;

public record DeleteRoleCommand(
    Guid Id
) : IRequest<bool>;