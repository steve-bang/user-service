/*
* Author: Steve Bang
* History:
* - [2025-05-02] - Created by mrsteve.bang@gmail.com
*/

using AutoMapper;

namespace Steve.ManagerHero.Application.Features.Permissions.Queries;

public class GetPermissionsByRoleQueryHandler(
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : IRequestHandler<GetPermissionsByRoleQuery, PaginatedList<PermissionDto>>
{
    public async Task<PaginatedList<PermissionDto>> Handle(GetPermissionsByRoleQuery request, CancellationToken cancellationToken)
    {
        // Get the role by id
        var role = await _unitOfWork.Roles.GetByIdAsync(request.RoleId, cancellationToken) ?? throw new RoleNotFoundException();

        // List all permissions for the role
        var permissions = await _unitOfWork.Permissions.GetPermissionsByRoleAsync(role, request.PageNumber, request.PageSize, cancellationToken);

        return new PaginatedList<PermissionDto>
        {
            Items = permissions.items.Select(_mapper.Map<PermissionDto>).ToList(),
            TotalCount = permissions.totalCount
        };
    }
}
