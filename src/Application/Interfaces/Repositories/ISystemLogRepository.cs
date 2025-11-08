
/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/


using Steve.ManagerHero.UserService.Domain.Entities;

namespace Steve.ManagerHero.UserService.Application.Interfaces.Repository;

public interface ISystemLogRepository : IRepository
{
    Task<SystemLogEntity> AddAsync(SystemLogEntity log, CancellationToken ct = default);
}