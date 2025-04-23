/*
* Author: Steve Bang
* History:
* - [2025-04-23] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Roles.Queries;

public record GetRolesByUserIdQuery(Guid UserId) : IRequest<IEnumerable<RoleDto>>;