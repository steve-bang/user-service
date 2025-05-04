/*
* Author: Steve Bang
* History:
* - [2025-05-04] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public record RemoveRolesFromUserCommand(
    Guid UserId,
    Guid[] RoleIds
) : IRequest<bool>;