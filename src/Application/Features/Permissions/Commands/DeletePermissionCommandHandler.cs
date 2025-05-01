/*
* Author: Steve Bang
* History:
* - [2025-05-01] - Updated by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.Application.Features.Permissions.Commands;

public class DeletePermissionCommandHandler(
    IUnitOfWork _unitOfWork
) : IRequestHandler<DeletePermissionCommand, bool>
{
    public async Task<bool> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
    {
        Permission permission = await _unitOfWork.Permissions.GetByIdAsync(request.Id) ?? throw ExceptionProviders.Permission.NotFoundException;

        var resultDelete = _unitOfWork.Permissions.Delete(permission);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return resultDelete;
    }
}