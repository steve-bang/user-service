/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Domain.Exception;

public class PasswordIncorrectException : BadRequestException
{
    public PasswordIncorrectException(
        string code = UserErrorCodes.PasswordIncorrect,
        string messsage = UserErrorMessages.PasswordIncorrectMessage
    ) : base(code, messsage)
    {

    }

}