/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.Constants;

public class OtpErrorCodes
{
    public const string Invalid = "OtpInvalid";

    public const string Expired = "OtpExpired";

    public const string AlreadyUsed = "OtpUsed";

    public const string Denied = "OtpDenied";
}