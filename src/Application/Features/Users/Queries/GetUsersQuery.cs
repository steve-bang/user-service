/*
* Author: Steve Bang
* History:
* - [2025-04-24] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public record GetUsersQuery : IRequest<PaginatedList<UserDto>>
{
    public string? Filter { get; init; } = null;
    public int PageNumber { get; init; } = PaginationConstant.PageNumberDefault;
    public int PageSize { get; init; } = PaginationConstant.PageSizeDefault;
}