/*
* Author: Steve Bang
* History:
* - [2025-05-01] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Permissions.Queries;

public record GetPermissionByIdQuery(Guid Id) : IRequest<PermissionDto>;