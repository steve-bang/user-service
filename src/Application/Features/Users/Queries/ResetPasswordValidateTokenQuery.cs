/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

public record ResetPasswordValidateTokenQuery(
    string Token 
) : IRequest<TokenValidateDto>;