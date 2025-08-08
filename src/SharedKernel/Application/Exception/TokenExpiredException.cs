/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.SharedKernel.Application.Exception;

public class TokenExpiredException : UnauthorizedException
{
    public TokenExpiredException() : base(
        code: "TokenExpired",
        message: "Your session has expired.")
    {
    }
}