/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using FluentValidation;
namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.EmailAddress)
            .NotEmpty()
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("The email address is in an invalid format.");

        RuleFor(x => x.SecondaryEmailAddress)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.SecondaryEmailAddress))
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("The secondary email address is in an invalid format.");

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

        RuleFor(x => x.DisplayName)
            .NotEmpty()
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("Display name is required.")
            .MaximumLength(100)
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("Display name must be less than 100 characters.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d{10,15}$")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithErrorCode(ExceptionProviders.InputInvalid)
            .WithMessage("Phone number must be in E.164 format (e.g., 1234567890).");

        When(x => x.Address is not null, () =>
        {
            RuleFor(x => x.Address!.Street)
                .NotEmpty()
                .WithErrorCode(ExceptionProviders.InputInvalid)
                .WithMessage("Street is required.");

            RuleFor(x => x.Address!.City)
                .NotEmpty()
                .WithErrorCode(ExceptionProviders.InputInvalid)
                .WithMessage("City is required.");

            RuleFor(x => x.Address!.State)
                .NotEmpty()
                .WithErrorCode(ExceptionProviders.InputInvalid)
                .WithMessage("State is required.");

            RuleFor(x => x.Address!.ZipCode)
                .NotEmpty()
                .WithErrorCode(ExceptionProviders.InputInvalid)
                .WithMessage("Postal code is required.")
                .Matches(@"^\d{4,10}$")
                .WithErrorCode(ExceptionProviders.InputInvalid)
                .WithMessage("Postal code format is invalid.");


            RuleFor(x => x.Address!.CountryCode)
                .NotEmpty()
                .WithErrorCode(ExceptionProviders.InputInvalid)
                .WithMessage("Country is required.");
        });
    }
}
