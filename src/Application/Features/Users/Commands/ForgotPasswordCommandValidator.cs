/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

using FluentValidation;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("A valid email is required.");
    }
}