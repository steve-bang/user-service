/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Application.Interfaces.Caching;

public interface ITokenCache
{
    string SetToken(string token, int timeToLiveMinutes);

    bool IsExistsToken(string token);

    void RevokeToken(string token);
}