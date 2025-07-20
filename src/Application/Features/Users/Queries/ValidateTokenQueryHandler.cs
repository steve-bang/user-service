/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

using System.Text.Encodings.Web;
using System.Web;
using Steve.ManagerHero.UserService.Application.Interfaces.Caching;
using Steve.ManagerHero.UserService.Helpers;

namespace Steve.ManagerHero.Application.Features.Users.Queries;

public class ValidateTokenQueryHandler(
    ITokenCache _tokenCache
) : IRequestHandler<ValidateTokenQuery, TokenValidateDto>
{
    public async Task<TokenValidateDto> Handle(ValidateTokenQuery request, CancellationToken cancellationToken)
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
                request.Purpose.ToString()
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