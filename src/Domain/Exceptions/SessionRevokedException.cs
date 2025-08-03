/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Domain.Exception;

public class SessionRevokedException : UnauthorizedException
{
    public SessionRevokedException() : base(SessionErrorCodes.SessionRevoked, SessionErrorMessages.RevokedMessage)
    {

    }

}