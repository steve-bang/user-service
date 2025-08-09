/*
* Author: Steve Bang
* History:
* - [2025-08-09] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.BuildingBlocks.CQRS;
using Steve.ManagerHero.UserService.Domain.Entities;

namespace Steve.ManagerHero.Application.Features.SystemLogs.Commands;

public record CreateSystemLogCommand(SystemLogEntity Logs) : ICommand<SystemLogEntity>;