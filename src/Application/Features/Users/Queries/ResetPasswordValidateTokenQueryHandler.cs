/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Application.Interfaces.Caching;
using Steve.ManagerHero.UserService.Helpers;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class ResetPasswordValidateTokenQueryHandler(
    ITokenCache _tokenCache,
    IConfiguration _configuration
) : IRequestHandler<ResetPasswordValidateTokenQuery, TokenValidateDto>
{
    public async Task<TokenValidateDto> Handle(ResetPasswordValidateTokenQuery request, CancellationToken cancellationToken)
    {
        try
        {
            await Task.CompletedTask;

            string token = request.Token;

            if (_tokenCache.IsExistsToken(token))
            {
                return TokenValidateDto.ValidToken();
            }

            string tokenDecrypt = EncryptionAESHelper.Decrypt(
                token, 
                _configuration.GetValue<string>("EncryptionSecretKey"), 
                EncryptionPurpose.ResetPassword.ToString()
            );

            if (!string.IsNullOrEmpty(tokenDecrypt))
                return TokenValidateDto.ValidToken();

            return TokenValidateDto.InValidToken();
        }
        catch (SecurityException)
        {
            return TokenValidateDto.InValidToken();
        }
    }
}