/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using FluentValidation;
using Steve.ManagerHero.UserService.Domain.Constants;
using Steve.ManagerHero.UserService.Domain.Services;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    private readonly IPasswordPolicy _passwordPolicy;

    public ChangePasswordCommandValidator(IPasswordPolicy passwordPolicy)
    {
        _passwordPolicy = passwordPolicy;

        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Password is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Password is required.")
            .Must(
                BeValidPassword
            ).WithErrorCode(UserErrorCodes.PasswordIncorrect)
            .WithMessage(x => GetPasswordPolicyMessage(x.NewPassword));

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.NewPassword)
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Passwords do not match.");

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