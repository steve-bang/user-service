/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using MediatR;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public record LogoutUserCommand(
    string AccessToken
) : IRequest<bool>;