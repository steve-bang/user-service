/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/


using Steve.ManagerHero.UserService.Helpers;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class ResetPasswordCommandHandler(
    IUserRepository _userRepository,
    IMediator _mediator
) : IRequestHandler<ResetPasswordCommand, bool>
{
    public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        string token = request.Token;

        var resetPasswordValidateTokenQuery = new ValidateTokenQuery(token, EncryptionPurpose.ResetPassword);
        var validResult = await _mediator.Send(resetPasswordValidateTokenQuery);

        // Checks if the valid result is true
        if (validResult.Valid)
        {
            UserPayloadEncrypt? userDecrypt = EncryptionAESHelper.DecryptObject<UserPayloadEncrypt>(
                token,
                EncryptionPurpose.ResetPassword.ToString()
            );

            if (userDecrypt is not null)
            {
                User? user = await _userRepository.GetByIdAsync(userDecrypt.Id) ?? throw ExceptionProviders.User.NotFoundException;

                // Update password
                user.UpdatePassword(request.NewPassword);

                _userRepository.Update(user);

                await _userRepository.UnitOfWork.SaveEntitiesAsync();

                return true;
            }
        }
        throw ExceptionProviders.Token.InvalidException;
    }
}