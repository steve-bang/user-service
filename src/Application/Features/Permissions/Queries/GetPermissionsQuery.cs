/*
* Author: Steve Bang
* History:
* - [2025-05-01] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Permissions.Queries;

public record GetPermissionsQuery : IRequest<PaginatedList<PermissionDto>>
{
    public string? Filter { get; init; } = null;
    public int PageNumber { get; init; } = PaginationConstant.PageNumberDefault;
    public int PageSize { get; init; } = PaginationConstant.PageSizeDefault;
}