

using Steve.ManagerHero.UserService.Application.Interfaces.Caching;

namespace Steve.ManagerHero.Application.Features.Sessions.Commands;

public class DeleteSessionByIdCommandHandler(
    IUnitOfWork _unitOfWork,
    ISessionCache _sessionCache
) : IRequestHandler<DeleteSessionByIdCommand>
{
    public async Task Handle(DeleteSessionByIdCommand request, CancellationToken cancellationToken)
    {
        Session? session = await _unitOfWork.Sessions.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new SessionNotFoundException();

        bool result = _unitOfWork.Sessions.Delete(session);

        if (result)
            _sessionCache.Clear(session.Id);
        else
            throw new SessionDeleteFailedException();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}