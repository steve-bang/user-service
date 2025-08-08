/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Application.Interfaces.Caching;
using Steve.ManagerHero.UserService.Helpers;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class VerificationEmailAddressCommandHandler(
    IUnitOfWork _unitOfWork,
    IMediator _mediator,
    IUserCache _userCache
) : IRequestHandler<VerificationEmailAddressCommand, bool>
{
    public async Task<bool> Handle(VerificationEmailAddressCommand request, CancellationToken cancellationToken)
    {
        string token = request.Token;

        var resetPasswordValidateTokenQuery = new ValidateTokenQuery(token, EncryptionPurpose.VerificationEmailAddress);
        var validResult = await _mediator.Send(resetPasswordValidateTokenQuery);

        // Checks if the valid result is true
        if (validResult.Valid == false)
            throw new InvalidTokenException();

        UserPayloadEncrypt? userDecrypt = EncryptionAESHelper.DecryptObject<UserPayloadEncrypt>(
            token,
            EncryptionPurpose.VerificationEmailAddress.ToString()
        );

        if (userDecrypt is null) throw new InvalidTokenException();

        User? user = await _unitOfWork.Users.GetByIdAsync(userDecrypt.Id, cancellationToken) ?? throw new UserNotFoundException();

        user.VerifyEmail();

        _unitOfWork.Users.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // Clear user from cache
        _userCache.ClearUserById(user.Id);

        return true;

    }
}
