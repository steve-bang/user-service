/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.Constants;

public class SessionErrorMessages
{
    public const string SessionNotFoundMessages = "The session was not found in the system.";

    public const string DeleteFailedMessage = "Delete the session was failed.";

    public const string RevokedMessage = "This session is no longer active.";

    public const string Expired = "Your session has expired.";

}