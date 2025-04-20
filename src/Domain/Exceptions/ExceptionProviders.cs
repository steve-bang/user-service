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
    public const string UserEmailOrPasswordIncorrect = "User.Login_Password_Failed";
    public const string UserNotFound = "User.Not_Found";
    public const string UserPasswordIncorrect = "User.Password_Incorrect";
    public const string EmailAlreadyVerified = "User.Email_already_verified";
    public const string TokenInvalid = "token_invalid";

    public static class User
    {
        public static BadRequestException EmailAlreadyExistsException => new(
            UserEmailAddressAlradyExists,
            "The email address already exists in the system. Please try another email address."
        );

        public static BadRequestException EmailAlreadyVerifiedException => new(
            EmailAlreadyVerified,
            "The email address already verified."
        );

        public static BadRequestException LoginPasswordFailedException => new(
            UserEmailOrPasswordIncorrect,
            "The email address or password incorrect in the system."
        );

        public static NotFoundDataException NotFoundException => new(
            UserNotFound,
            "The user not found in the system."
        );

        public static BadRequestException PasswordIncorrectException => new(
            UserPasswordIncorrect,
            "The password incorrect."
        );
    }

    public static class Token
    {
        public static BadRequestException InvalidException => new(
            TokenInvalid,
            "Token expired or invalid."
        );
    }
}