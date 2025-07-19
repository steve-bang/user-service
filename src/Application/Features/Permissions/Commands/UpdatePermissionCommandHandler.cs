/*
* Author: Steve Bang
* History:
* - [2025-05-01] - Updated by mrsteve.bang@gmail.com
*/


using AutoMapper;

namespace Steve.ManagerHero.Application.Features.Permissions.Commands;

public class UpdatePermissionCommandHandler(
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : IRequestHandler<UpdatePermissionCommand, PermissionDto>
{
    public async Task<PermissionDto> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        Permission permission = await _unitOfWork.Permissions.GetByIdAsync(request.Id) ?? throw new PermissionNotFoundException();

        permission.Update(
            request.Code,
            request.Name,
            request.Description
        );

        _unitOfWork.Permissions.Update(permission);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PermissionDto>(permission);
    }
}