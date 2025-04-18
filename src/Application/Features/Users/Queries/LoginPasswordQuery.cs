/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using MediatR;
using Steve.ManagerHero.UserService.Application.DTO;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public record LoginPasswordQuery(
    string EmailAddress,
    string Password
) : IRequest<AuthenticationResponseDto>;