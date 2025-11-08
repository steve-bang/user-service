/*
* Author: Steve Bang
* History:
* - [2025-08-14] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Domain.Exception;

public class OtpDeniedException : BadRequestException
{
    public OtpDeniedException() : base(OtpErrorCodes.Denied, OtpErrorMessages.DeniedMessage)
    { }
}