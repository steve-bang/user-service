/*
* Author: Steve Bang
* History:
* - [2025-08-09] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.BuildingBlocks.CQRS;
using Steve.ManagerHero.UserService.Domain.Entities;

namespace Steve.ManagerHero.Application.Features.SystemLogs.Commands;

public class CreateSystemLogCommandHandler(
    IUnitOfWork _unitOfWork
) : ICommandHandler<CreateSystemLogCommand, SystemLogEntity>
{
    public async Task<SystemLogEntity> Handle(CreateSystemLogCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.SystemLogs.AddAsync(request.Logs, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request.Logs;
    }
}