/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public record SendVerificationEmailLinkCommand(
    Guid UserId
) : IRequest<bool>;