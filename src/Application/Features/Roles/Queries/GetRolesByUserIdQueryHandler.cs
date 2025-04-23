/*
* Author: Steve Bang
* History:
* - [2025-04-23] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.Application.Features.Roles.Queries;

public class GetRolesByUserIdQueryHandler(
    IRoleRepository _roleRepository
) : IRequestHandler<GetRolesByUserIdQuery, IEnumerable<RoleDto>>
{
    public async Task<IEnumerable<RoleDto>> Handle(GetRolesByUserIdQuery request, CancellationToken cancellationToken)
    {
        var roles = await _roleRepository.GetRolesByUserId(request.UserId);

        return roles.Select(role => new RoleDto(role.Id, role.Name, role.Description, role.CreatedAt));
    }
}