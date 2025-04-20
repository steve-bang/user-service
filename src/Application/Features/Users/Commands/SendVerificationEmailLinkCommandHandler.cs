/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class SendVerificationEmailLinkCommandHandler(
    IUserRepository _userRepository,
    IMediator _mediator
) : IRequestHandler<SendVerificationEmailLinkCommand, bool>
{
    public async Task<bool> Handle(SendVerificationEmailLinkCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken) ?? throw ExceptionProviders.User.NotFoundException;

        if (user.IsEmailVerified)
            throw ExceptionProviders.User.EmailAlreadyVerifiedException;

        EmailVerificationEvent emailVerification = new(user);
        _ = _mediator.Publish(emailVerification);

        return true;
    }
}