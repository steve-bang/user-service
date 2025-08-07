/*
* Author: Steve Bang
* History:
* - [2025-08-03] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.Interfaces.Caching;

public interface IPermissionCache
{
    void SetPermissionsByUserId(Guid userId, string[] permissions);

    bool GetPermissionsByUserId(Guid userId, out string[]? permissions);

    void Clear(Guid permissionId);

    void ClearByUserId(Guid userId);

}