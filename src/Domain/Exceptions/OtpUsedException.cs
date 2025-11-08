/*
* Author: Steve Bang
* History:
* - [2025-08-14] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Domain.Exception;

public class OtpUsedException : BadRequestException
{
    public OtpUsedException() : base(OtpErrorCodes.AlreadyUsed, OtpErrorMessages.AlreadyUsedMessage)
    { }
}