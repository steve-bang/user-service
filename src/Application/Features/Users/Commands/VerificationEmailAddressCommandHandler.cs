/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Helpers;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class VerificationEmailAddressCommandHandler(
    IUserRepository _userRepository,
    IMediator _mediator
) : IRequestHandler<VerificationEmailAddressCommand, bool>
{
    public async Task<bool> Handle(VerificationEmailAddressCommand request, CancellationToken cancellationToken)
    {
        string token = request.Token;

        var resetPasswordValidateTokenQuery = new ValidateTokenQuery(token, EncryptionPurpose.VerificationEmailAddress);
        var validResult = await _mediator.Send(resetPasswordValidateTokenQuery);

        // Checks if the valid result is true
        if (validResult.Valid)
        {
            UserPayloadEncrypt? userDecrypt = EncryptionAESHelper.DecryptObject<UserPayloadEncrypt>(
                token,
                EncryptionPurpose.VerificationEmailAddress.ToString()
            );

            if (userDecrypt is not null)
            {
                User? user = await _userRepository.GetByIdAsync(userDecrypt.Id) ?? throw ExceptionProviders.User.NotFoundException;

                user.VerifyEmail();

                _userRepository.Update(user);

                await _userRepository.UnitOfWork.SaveEntitiesAsync();

                return true;
            }
        }
        throw ExceptionProviders.Token.InvalidException;
    }
}
