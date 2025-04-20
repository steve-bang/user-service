/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Helpers;

public record ValidateTokenQuery(
    string Token,
    EncryptionPurpose Purpose
) : IRequest<TokenValidateDto>;