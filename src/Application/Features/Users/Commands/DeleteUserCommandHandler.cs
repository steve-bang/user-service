/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class DeleteUserCommandHandler(
    IUnitOfWork _unitOfWork
) : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken) ?? throw ExceptionProviders.User.NotFoundException;

        bool result = _unitOfWork.Users.Delete(user);

        await _unitOfWork.SaveChangesAsync();

        return result;
    }
}