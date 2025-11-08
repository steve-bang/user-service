/*
* Author: Steve Bang
* History:
* - [2025-05-04] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Application.Interfaces.Caching;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class RemoveRolesFromUserCommandHandler(
    IUnitOfWork _unitOfWork,
    IPermissionCache _permissionCache
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

        // Save changes to the database
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Clear cache permission code with user
        _permissionCache.ClearByUserId(user.Id);

        return true;
    }
}