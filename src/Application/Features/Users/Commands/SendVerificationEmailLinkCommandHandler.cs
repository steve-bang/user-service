/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Exception;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class SendVerificationEmailLinkCommandHandler(
    IUnitOfWork _unitOfWork,
    IMediator _mediator
) : IRequestHandler<SendVerificationEmailLinkCommand, bool>
{
    public async Task<bool> Handle(SendVerificationEmailLinkCommand request, CancellationToken cancellationToken)
    {
        User user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken) ?? throw new UserNotFoundException();

        if (user.IsEmailVerified)
            throw new EmailAlreadyVerifiedException();

        EmailVerificationEvent emailVerification = new(user);
        _ = _mediator.Publish(emailVerification, cancellationToken);

        return true;
    }
}