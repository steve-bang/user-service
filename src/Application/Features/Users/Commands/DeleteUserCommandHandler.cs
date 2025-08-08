/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/


using Steve.ManagerHero.UserService.Application.Interfaces.Caching;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class DeleteUserCommandHandler(
    IUnitOfWork _unitOfWork,
    IUserCache _userCache
) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken) ?? throw new UserNotFoundException();

        bool result = _unitOfWork.Users.Delete(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Clear user from cache
        _userCache.ClearUserById(user.Id);

        return result;
    }
}