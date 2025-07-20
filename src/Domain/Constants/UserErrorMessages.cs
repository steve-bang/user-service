/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.Constants;

public class UserErrorMessages
{
    public const string InvalidCredentialMessage = "The email address or password incorrect in the system.";

    public const string EmailAlreadyVerifiedMessage = "The email address already exists in the system.";

    public const string EmailAlreadyExistsMessage = "The email address already exists in the system. Please try another email address.";

    public const string UserNotFoundMessage = "The user not found in the system.";

    public const string PasswordIncorrectMessage = "The password is incorrect.";

    public const string UserAlreadyHasRoleMessage = "User already has this role.";
}