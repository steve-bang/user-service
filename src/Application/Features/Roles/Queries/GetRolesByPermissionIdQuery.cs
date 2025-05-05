/*
* Author: Steve Bang
* History:
* - [2025-05-01] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Roles.Queries;

public record GetRolesByPermissionIdQuery(Guid PermissionId) : IRequest<IEnumerable<RoleDto>>;