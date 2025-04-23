/*
* Author: Steve Bang
* History:
* - [2025-04-22] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.Application.Features.Roles.Commands;

public class CreateRoleCommandHandler(
    IRoleRepository _roleRepository
) : IRequestHandler<CreateRoleCommand, RoleDto>
{
    public async Task<RoleDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Role(
            request.Name,
            request.Description
        );

        var roleCreated = await _roleRepository.CreateAsync(role, cancellationToken);

        await _roleRepository.UnitOfWork.SaveEntitiesAsync();

        return new RoleDto(roleCreated.Id, roleCreated.Name, roleCreated.Description, roleCreated.CreatedAt);
    }
}