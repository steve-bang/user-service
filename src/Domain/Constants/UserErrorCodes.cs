/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.Constants;

public class UserErrorCodes
{
    public const string InvalidCredential = nameof(InvalidCredential);

    public const string EmailAlreadyVerified = nameof(EmailAlreadyVerified);

    public const string EmailAlreadyExists = nameof(EmailAlreadyExists);

    public const string UserNotFound = nameof(UserNotFound);

    public const string PasswordIncorrect = nameof(PasswordIncorrect);

    public const string UserAlreadyHasRole = nameof(UserAlreadyHasRole);
}