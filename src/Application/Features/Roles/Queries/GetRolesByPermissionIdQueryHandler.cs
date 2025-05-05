/*
* Author: Steve Bang
* History:
* - [2025-05-01] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.Application.Features.Roles.Queries;

public class GetRolesByPermissionIdQueryHandler(
    IUnitOfWork _unitOfWork
) : IRequestHandler<GetRolesByPermissionIdQuery, IEnumerable<RoleDto>>
{
    public async Task<IEnumerable<RoleDto>> Handle(GetRolesByPermissionIdQuery request, CancellationToken cancellationToken)
    {
        var roles = await _unitOfWork.Roles.GetRolesByPermissionId(request.PermissionId);

        return roles.Select(role => new RoleDto(role.Id, role.Name, role.Description, role.CreatedAt));
    }
}