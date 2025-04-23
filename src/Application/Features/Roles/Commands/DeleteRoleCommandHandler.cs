/*
* Author: Steve Bang
* History:
* - [2025-04-22] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.Application.Features.Roles.Commands;

public class DeleteRoleCommandHandler(
    IUnitOfWork _unitOfWork
) : IRequestHandler<DeleteRoleCommand, bool>
{
    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        Role role = await _unitOfWork.Roles.GetByIdAsync(request.Id, cancellationToken) ?? throw ExceptionProviders.Role.NotFoundException;

        var result = _unitOfWork.Roles.Delete(role, cancellationToken);

        await _unitOfWork.SaveChangesAsync();

        return result;
    }
}