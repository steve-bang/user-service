/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.Constants;

public class OtpErrorMessages
{
    public const string InvalidMessage = "The OTP code is invalid.";

    public const string ExpiredMessage = "The OTP code has expired.";

    public const string AlreadyUsedMessage = "The OTP code has already been used.";

    public const string DeniedMessage = "The OTP reached the maximum retry limit.";

}