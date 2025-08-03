/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Application.Auth;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class LogoutUserCommandHandler(
    IUnitOfWork _unitOfWork,
    IJwtHandler _jwtHandler
) : IRequestHandler<LogoutUserCommand, bool>
{
    public async Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        Guid sessionId = _jwtHandler.ExtraSessionId(request.AccessToken);

        var session = await _unitOfWork.Sessions.GetByIdAsync(sessionId, cancellationToken);

        if (session is null) return false;

        session.Revoked();

        // Delete session
        _unitOfWork.Sessions.Update(session);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}