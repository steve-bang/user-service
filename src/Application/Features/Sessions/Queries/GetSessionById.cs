/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/


using Steve.ManagerHero.UserService.Application.DTOs;

namespace Steve.ManagerHero.Application.Features.Sessions.Queries;

public record GetSessionByIdQuery(Guid Id) : IRequest<SessionDto>;