/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public record AuthExternalCallbackCommand(
    IdentityProvider Provider,
    string Code,
    string? State
) : IRequest<AuthenticationResponseDto>;