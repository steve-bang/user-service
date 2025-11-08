/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using FluentValidation;
using Steve.ManagerHero.UserService.Domain.Constants;
using Steve.ManagerHero.UserService.Domain.Services;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly IPasswordPolicy _passwordPolicy;

    public RegisterUserCommandValidator(IPasswordPolicy passwordPolicy)
    {
        _passwordPolicy = passwordPolicy;

        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("A valid email is required.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Password is required.")
            .Must(
                BeValidPassword
            ).WithErrorCode(UserErrorCodes.PasswordIncorrect)
            .WithMessage(x => GetPasswordPolicyMessage(x.Password));

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Passwords do not match.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("First name is required.")
            .MaximumLength(50)
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("First name must be less than 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Last name is required.")
            .MaximumLength(50)
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Last name must be less than 50 characters.");
    }

    private bool BeValidPassword(string password)
    {
        var result = _passwordPolicy.Validate(password);
        return result.IsValid;
    }

    private string? GetPasswordPolicyMessage(string password)
    {
        var result = _passwordPolicy.Validate(password);
        return result.IsValid
            ? string.Empty
            : result.Message;
    }
}