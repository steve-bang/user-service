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
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("The email address is in an invalid format.");

        RuleFor(x => x.SecondaryEmailAddress)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.SecondaryEmailAddress))
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("The secondary email address is in an invalid format.");

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

        RuleFor(x => x.DisplayName)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Display name is required.")
            .MaximumLength(100)
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Display name must be less than 100 characters.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\d{10,15}$")
            .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Phone number must be in E.164 format (e.g., 1234567890).");

        When(x => x.Address is not null, () =>
        {
            RuleFor(x => x.Address!.Street)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.InputInvalid)
                .WithMessage("Street is required.");

            RuleFor(x => x.Address!.City)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.InputInvalid)
                .WithMessage("City is required.");

            RuleFor(x => x.Address!.State)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.InputInvalid)
                .WithMessage("State is required.");

            RuleFor(x => x.Address!.ZipCode)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.InputInvalid)
                .WithMessage("Postal code is required.")
                .Matches(@"^\d{4,10}$")
                .WithErrorCode(ErrorCodes.InputInvalid)
                .WithMessage("Postal code format is invalid.");


            RuleFor(x => x.Address!.CountryCode)
                .NotEmpty()
                .WithErrorCode(ErrorCodes.InputInvalid)
                .WithMessage("Country is required.");
        });
    }
}
