/*
* Author: Steve Bang
* History:
* - [2025-05-02] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Permissions.Queries;

public record GetPermissionsByRoleQuery : IRequest<PaginatedList<PermissionDto>>
{
    public Guid RoleId { get; init; } = Guid.Empty;
    public int PageNumber { get; init; } = PaginationConstant.PageNumberDefault;
    public int PageSize { get; init; } = PaginationConstant.PageSizeDefault;
}