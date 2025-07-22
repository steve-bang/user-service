/*
* Author: Steve Bang
* History:
* - [2025-05-05] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Infrastructure.Auth.External;

namespace Steve.ManagerHero.UserService.Application.Interfaces.Services;

public interface IExternalAuthService
{
    string GetLoginUrl(string state);

    Task<OAuthUser> VerifyTokenAsync(string token);
}