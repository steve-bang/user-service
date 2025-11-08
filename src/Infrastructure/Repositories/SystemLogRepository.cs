/*
* Author: Steve Bang
* History:
* - [2025-08-09] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Entities;

namespace Steve.ManagerHero.UserService.Infrastructure.Repository;

public class SystemLogRepository(UserAppContext _context) : ISystemLogRepository
{
    public async Task<SystemLogEntity> AddAsync(SystemLogEntity log, CancellationToken ct = default)
    {
        var systemLogAdded = await _context.SystemLogs.AddAsync(log, ct);

        return systemLogAdded.Entity;
    }
}