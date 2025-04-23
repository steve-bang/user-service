/*
* Author: Steve Bang
* History:
* - [2025-04-22] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.Application.Features.Roles.Commands;

public class UpdateRoleCommandHandler(
    IRoleRepository _roleRepository
) : IRequestHandler<UpdateRoleCommand, RoleDto>
{
    public async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        Role role = await _roleRepository.GetByIdAsync(request.Id) ?? throw ExceptionProviders.Role.NotFoundException;

        role.Update(request.Name, request.Description);

        _roleRepository.Update(role);

        await _roleRepository.UnitOfWork.SaveEntitiesAsync();

        return new RoleDto(role.Id, role.Name, role.Description, role.CreatedAt);
    }
}