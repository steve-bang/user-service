

using Steve.ManagerHero.UserService.Application.Interfaces.Caching;

namespace Steve.ManagerHero.Application.Features.Sessions.Commands;

public class RevokeSessionByIdCommandHandler(
    IUnitOfWork _unitOfWork,
    ISessionCache _sessionCache
) : IRequestHandler<RevokeSessionByIdCommand>
{
    public async Task Handle(RevokeSessionByIdCommand request, CancellationToken cancellationToken)
    {
        Session? session = await _unitOfWork.Sessions.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new SessionNotFoundException();

        session.Revoke();

        _unitOfWork.Sessions.Update(session);

        _sessionCache.Clear(session.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}