/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using FluentValidation;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {

        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Password must be at least 8 characters.")
            .MaximumLength(100)
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Password must be less than 100 characters.");

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