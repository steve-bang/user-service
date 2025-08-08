/*
* Author: Steve Bang
* History:
* - [2025-05-05] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.Interfaces.Services;

public interface IAuditService
{
    Task CreateAsync(
        Guid userId, 
        string action, 
        string entityType, 
        string entityId,
        Dictionary<string, object?> oldValues,
        Dictionary<string, object?> newValues,
        string ipAddress, string userAgent
    );
    
    Task<List<Audit>> GetAuditsForEntity(string entityId, string tableName);
}