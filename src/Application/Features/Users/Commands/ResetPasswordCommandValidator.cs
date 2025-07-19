/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

using FluentValidation;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Token reset password is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Password must be at least 8 characters.")
            .MaximumLength(100)
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Password must be less than 100 characters.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.NewPassword)
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Passwords do not match.");
    }
}