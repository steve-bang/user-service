/*
* Author: Steve Bang
* History:
* - [2025-04-22] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.Application.Features.Roles.Commands;

public class DeleteRoleCommandHandler(
    IRoleRepository _roleRepository
) : IRequestHandler<DeleteRoleCommand, bool>
{
    public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        Role role = await _roleRepository.GetByIdAsync(request.Id) ?? throw ExceptionProviders.Role.NotFoundException;

        var result = _roleRepository.Delete(role);

        await _roleRepository.UnitOfWork.SaveEntitiesAsync();

        return result;
    }
}