/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.Exceptions;

public class ExceptionProviders
{
    public const string InputInvalid = "Input_Invalid";

    public const string UserEmailAddressAlradyExists = "User.Email_Address_Already_Exists";

    public static class User
    {
        public static BadRequestException EmailAlreadyExistsException => new(
            UserEmailAddressAlradyExists,
            "The email address already exists in the system. Please try another email address."
        );
    }
}