/*
* Author: Steve Bang
* History:
* - [2025-04-22] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.Application.Features.Roles.Commands;

public class AssignUserToRoleCommandHandler(
    IRoleRepository _roleRepository,
    IUserRepository _userRepository
) : IRequestHandler<AssignUserToRoleCommand, bool>
{
    public async Task<bool> Handle(AssignUserToRoleCommand request, CancellationToken cancellationToken)
    {
        Role role = await _roleRepository.GetByIdAsync(request.RoleId) ?? throw ExceptionProviders.Role.NotFoundException;

        User user = await _userRepository.GetByIdAsync(request.UserId) ?? throw ExceptionProviders.User.NotFoundException;

        user.AddRole(role);

        _userRepository.Update(user);

        await _userRepository.UnitOfWork.SaveEntitiesAsync();

        return true;
    }
}