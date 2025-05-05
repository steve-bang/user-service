/*
* Author: Steve Bang
* History:
* - [2025-04-22] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.Application.Features.Roles.Queries;

public class GetRoleByIdQueryHandler(
    IUnitOfWork _unitOfWork
) : IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        Role role = await _unitOfWork.Roles.GetByIdAsync(request.Id) ?? throw ExceptionProviders.Role.NotFoundException;

        return new RoleDto(role.Id, role.Name, role.Description, role.CreatedAt);
    }
}