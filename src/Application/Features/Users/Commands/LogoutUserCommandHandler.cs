/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using MediatR;
using Steve.ManagerHero.UserService.Application.Interfaces.Repository;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class LogoutUserCommandHandler(
    ISessionRepository _sessionRepository
) : IRequestHandler<LogoutUserCommand, bool>
{
    public async Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var session = await _sessionRepository.GetByAccessTokenAsync(request.AccessToken, cancellationToken);

        if (session is null) return false;

        // Delete session
        _sessionRepository.Delete(session);

        await _sessionRepository.UnitOfWork.SaveEntitiesAsync();

        return true;
    }
}