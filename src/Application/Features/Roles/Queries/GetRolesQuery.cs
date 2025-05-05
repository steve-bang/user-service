/*
* Author: Steve Bang
* History:
* - [2025-05-04] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Roles.Queries;

public record GetRolesQuery : IRequest<PaginatedList<RoleDto>>
{
    public string? Filter { get; init; } = null;
    public int PageNumber { get; init; } = PaginationConstant.PageNumberDefault;
    public int PageSize { get; init; } = PaginationConstant.PageSizeDefault;
}