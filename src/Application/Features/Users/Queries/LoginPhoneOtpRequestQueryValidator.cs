/*
* Author: Steve Bang
* History:
* - [2025-08-10] - Created by mrsteve.bang@gmail.com
*/

using FluentValidation;
using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class LoginPhoneOtpRequestQueryValidator : AbstractValidator<LoginPhoneOtpRequestQuery>
{

    public LoginPhoneOtpRequestQueryValidator()
    {

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithErrorCode(ErrorCodes.InputInvalid)
            .WithMessage("Phone number is required.")
            .Matches(Regex.PhoneNumberRegex)
            .WithErrorCode(UserErrorCodes.PhoneNumberInvalid)
            .WithMessage(UserErrorMessages.PhoneNumberInvalidMessage);
    }
}