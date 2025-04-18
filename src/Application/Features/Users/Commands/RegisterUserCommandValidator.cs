/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using FluentValidation;
using Steve.ManagerHero.UserService.Domain.Exceptions;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("A valid email is required.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("Password must be at least 8 characters.")
            .MaximumLength(100)
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("Password must be less than 100 characters.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("Passwords do not match.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("First name is required.")
            .MaximumLength(50)
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("First name must be less than 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("Last name is required.")
            .MaximumLength(50)
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("Last name must be less than 50 characters.");
    }
}