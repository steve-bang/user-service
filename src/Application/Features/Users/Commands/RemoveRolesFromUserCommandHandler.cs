/*
* Author: Steve Bang
* History:
* - [2025-05-04] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class RemoveRolesFromUserCommandHandler(
    IUnitOfWork _unitOfWork
) : IRequestHandler<RemoveRolesFromUserCommand, bool>
{

    public async Task<bool> Handle(RemoveRolesFromUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken) ?? throw new UserNotFoundException();

        var roles = await _unitOfWork.Roles.GetByIdsAsync(request.RoleIds, cancellationToken);
        if (roles.Count() != request.RoleIds.Length)
        {
            throw new RoleNotFoundException();
        }

        // Remove roles
        user.RemoveRoles(roles);

        // Update the user in the repository
        //_unitOfWork.Users.Update(user, cancellationToken);

        // Save changes to the database
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}