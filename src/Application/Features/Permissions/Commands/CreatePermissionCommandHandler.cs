/*
* Author: Steve Bang
* History:
* - [2025-05-01] - Created by mrsteve.bang@gmail.com
*/


using AutoMapper;

namespace Steve.ManagerHero.Application.Features.Permissions.Commands;

public class CreatePermissionCommandHandler(
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : IRequestHandler<CreatePermissionCommand, PermissionDto>
{
    public async Task<PermissionDto> Handle(CreatePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = new Permission(
            request.Code,
            request.Name,
            request.Description
        );

        var permissionCreated = await _unitOfWork.Permissions.CreateAsync(permission, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PermissionDto>(permissionCreated);
    }
}