/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.Application.Features.Users.Queries;

public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;