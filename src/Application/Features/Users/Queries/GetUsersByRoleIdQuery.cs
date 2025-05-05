/*
* Author: Steve Bang
* History:
* - [2025-05-04] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public record GetUsersByRoleIdQuery : IRequest<PaginatedList<UserDto>>
{
    public Guid RoleId { get; init; }
    public int PageNumber { get; init; } = PaginationConstant.PageNumberDefault;
    public int PageSize { get; init; } = PaginationConstant.PageSizeDefault;
}