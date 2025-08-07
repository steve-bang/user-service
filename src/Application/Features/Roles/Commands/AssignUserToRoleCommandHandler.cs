/*
* Author: Steve Bang
* History:
* - [2025-04-22] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Application.Interfaces.Caching;

namespace Steve.ManagerHero.Application.Features.Roles.Commands;

public class AssignUserToRoleCommandHandler(
    IUnitOfWork _unitOfWork,
    IPermissionCache _permissionCache
) : IRequestHandler<AssignUserToRoleCommand, bool>
{
    public async Task<bool> Handle(AssignUserToRoleCommand request, CancellationToken cancellationToken)
    {
        Role role = await _unitOfWork.Roles.GetByIdAsync(request.RoleId, cancellationToken) ?? throw new RoleNotFoundException();

        User user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken) ?? throw new UserNotFoundException();

        user.AddRole(role);

        _unitOfWork.Users.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _permissionCache.ClearByUserId(user.Id);

        return true;
    }
}