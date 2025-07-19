/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using System.Net;
using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Domain.Exception;

public class InvalidCredentialException : UnauthorizedException
{
    public InvalidCredentialException(
    ) : base(UserErrorCodes.InvalidCredential, UserErrorMessages.InvalidCredentialMessage)
    {
        HttpCode = HttpStatusCode.Unauthorized;
    }

}