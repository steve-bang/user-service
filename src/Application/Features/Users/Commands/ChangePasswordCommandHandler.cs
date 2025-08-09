/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Services;
using Steve.ManagerHero.UserService.Infrastructure.Security;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class ChangePasswordCommandHandler(
    IUnitOfWork _unitOfWork,
    IPasswordHasher _passwordHasher,
    IPasswordHistoryPolicyService _passwordHistoryPolicy
) : IRequestHandler<ChangePasswordCommand, bool>
{
    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        User user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken) ?? throw new UserNotFoundException();


        // Update password
        user.ChangePassword(request.CurrentPassword, request.NewPassword, _passwordHasher, _passwordHistoryPolicy);

        // Update data in state
        _unitOfWork.Users.Update(user, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}