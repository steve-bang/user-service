/*
* Author: Steve Bang
* History:
* - [2025-05-02] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Permissions.Commands;

public class RemovePermissionsFromRoleCommandHandler(
    IUnitOfWork _unitOfWork
) : IRequestHandler<RemovePermissionsFromRoleCommand, bool>
{

    public async Task<bool> Handle(RemovePermissionsFromRoleCommand request, CancellationToken cancellationToken)
    {
        Role role = await _unitOfWork.Roles.GetByIdAsync(request.RoleId, cancellationToken) ?? throw new RoleNotFoundException();

        var permissions = await _unitOfWork.Permissions.GetByIdsAsync(request.PermissionIds, cancellationToken);
        if (permissions.Count() != request.PermissionIds.Length)
        {
            throw new PermissionNotFoundException();
        }

        // Add permissions to the role
        role.RemovePermissions(permissions);

        // Update the role in the repository
        _unitOfWork.Roles.Update(role, cancellationToken);

        // Save changes to the database
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}