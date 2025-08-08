/*
* Author: Steve Bang
* History:
* - [2025-04-22] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.Application.Features.Roles.Commands;

public class UpdateRoleCommandHandler(
    IUnitOfWork _unitOfWork
) : IRequestHandler<UpdateRoleCommand, RoleDto>
{
    public async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        Role role = await _unitOfWork.Roles.GetByIdAsync(request.Id, cancellationToken) ?? throw new RoleNotFoundException();

        role.Update(request.Name, request.Description);

        _unitOfWork.Roles.Update(role, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new RoleDto(role.Id, role.Name, role.Description, role.CreatedAt);
    }
}