/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class DeleteUserCommandHandler(
    IUserRepository _userRepository
) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken) ?? throw ExceptionProviders.User.NotFoundException;

        bool result = _userRepository.Delete(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync();

        return result;
    }
}