/*
* Author: Steve Bang
* History:
* - [2025-08-10] - Created by mrsteve.bang@gmail.com
*/

using FluentValidation;
using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class LoginPhoneOtpVerifyQueryValidator : AbstractValidator<LoginPhoneOtpVerifyQuery>
{

    public LoginPhoneOtpVerifyQueryValidator()
    {

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Phone number is required.")
            .Matches(Regex.PhoneNumberRegex)
            .WithErrorCode(UserErrorCodes.PhoneNumberInvalid)
            .WithMessage(UserErrorMessages.PhoneNumberInvalidMessage);

        RuleFor(x => x.OtpCode)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Otp code is required.");
    }
}