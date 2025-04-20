/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class ChangePasswordCommandHandler(
    IUserRepository _userRepository
) : IRequestHandler<ChangePasswordCommand, bool>
{
    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken) ?? throw ExceptionProviders.User.NotFoundException;

        // Update password
        user.ChangePassword(request.CurrentPassword, request.NewPassword);

        // Update data in state
        _userRepository.Update(user);

        // Clear session

        await _userRepository.UnitOfWork.SaveEntitiesAsync();

        return true;
    }
}