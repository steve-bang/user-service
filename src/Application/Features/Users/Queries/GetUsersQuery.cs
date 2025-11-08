/*
* Author: Steve Bang
* History:
* - [2025-04-24] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.BuildingBlocks.Page;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class GetUsersQuery : PageRequest, IRequest<PaginatedList<UserDto>>
{
    public string? Filter { get; init; } = null;
}