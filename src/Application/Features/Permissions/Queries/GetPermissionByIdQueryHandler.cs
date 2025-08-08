/*
* Author: Steve Bang
* History:
* - [2025-05-01] - Created by mrsteve.bang@gmail.com
*/


using AutoMapper;

namespace Steve.ManagerHero.Application.Features.Permissions.Queries;

public class GetPermissionByIdQueryHandler(
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : IRequestHandler<GetPermissionByIdQuery, PermissionDto>
{
    public async Task<PermissionDto> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
    {
        Permission Permission = await _unitOfWork.Permissions.GetByIdAsync(request.Id) ?? throw new PermissionNotFoundException();

        return _mapper.Map<PermissionDto>(Permission);
    }
}