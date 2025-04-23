/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.Exceptions;

public class ExceptionProviders
{
    public const string InputInvalid = "Input_Invalid";
    public const string TokenInvalid = "token_invalid";
    public const string RoleNotFound = "role.not_found";

    public static class User
    {
        public const string UserEmailAddressAlradyExists = "User.Email_Address_Already_Exists";
        public const string UserEmailOrPasswordIncorrect = "User.Login_Password_Failed";
        public const string UserNotFound = "User.Not_Found";
        public const string UserPasswordIncorrect = "User.Password_Incorrect";
        public const string UserAlreadyHasRole = "user.already_has_role";
        public const string EmailAlreadyVerified = "User.Email_already_verified";

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
            "The user was not found in the system."
        );

        public static BadRequestException PasswordIncorrectException => new(
            UserPasswordIncorrect,
            "The password incorrect."
        );

        public static BadRequestException AlreadyHasRoleException => new(
            UserAlreadyHasRole,
            "User already has this role."
        );
    }

    public static class Token
    {
        public static BadRequestException InvalidException => new(
            TokenInvalid,
            "Token expired or invalid."
        );
    }

    public static class Role
    {
        public static NotFoundDataException NotFoundException => new(
            RoleNotFound,
            "The role was not found in the system."
        );
    }
}