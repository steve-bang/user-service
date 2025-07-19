/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/


using Steve.ManagerHero.UserService.Helpers;

namespace Steve.ManagerHero.Application.Features.Users.Commands;

public class ResetPasswordCommandHandler(
    IUnitOfWork _unitOfWork,
    IMediator _mediator
) : IRequestHandler<ResetPasswordCommand, bool>
{
    public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        string token = request.Token;

        var resetPasswordValidateTokenQuery = new ValidateTokenQuery(token, EncryptionPurpose.ResetPassword);
        var validResult = await _mediator.Send(resetPasswordValidateTokenQuery, cancellationToken);

        // Checks if the valid result is true
        if (validResult.Valid)
        {
            UserPayloadEncrypt? userDecrypt = EncryptionAESHelper.DecryptObject<UserPayloadEncrypt>(
                token,
                EncryptionPurpose.ResetPassword.ToString()
            );

            if (userDecrypt is not null)
            {
                User? user = await _unitOfWork.Users.GetByIdAsync(userDecrypt.Id, cancellationToken) ?? throw new UserNotFoundException();

                // Update password
                user.UpdatePassword(request.NewPassword);

                _unitOfWork.Users.Update(user);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return true;
            }
        }
        throw new InvalidTokenException();
    }
}