/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Events;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class ForgotPasswordCommandHandler(
    IUnitOfWork _unitOfWork,
    IMediator _mediator
) : IRequestHandler<ForgotPasswordCommand, bool>
{
    public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        User? user = await _unitOfWork.Users.GetByEmailAsync(request.EmailAddress, cancellationToken);

        if (user is null) return false;

        ForgotPasswordEvent forgotPasswordEvent = new ForgotPasswordEvent(user);
        _ = _mediator.Publish(forgotPasswordEvent);

        return true;
    }
}