/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.Constants;

public class SessionErrorCodes
{
    public const string SessionNotFound = nameof(SessionNotFound);

    public const string DeleteFailed = "SessionDeleteFailed";

    public const string SessionRevoked = nameof(SessionRevoked);

    public const string SessionExpired = nameof(SessionExpired);

}