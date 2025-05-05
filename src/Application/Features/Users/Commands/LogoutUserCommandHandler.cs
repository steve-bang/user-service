/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class LogoutUserCommandHandler(
    IUnitOfWork _unitOfWork
) : IRequestHandler<LogoutUserCommand, bool>
{
    public async Task<bool> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        var session = await _unitOfWork.Sessions.GetByAccessTokenAsync(request.AccessToken, cancellationToken);

        if (session is null) return false;

        // Delete session
        _unitOfWork.Sessions.Delete(session);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}