/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using System.Net;

namespace Steve.ManagerHero.UserService.Domain.Exceptions;
public class TokenExpiredException : ManagerHeroException
{
    public TokenExpiredException() : base("token_expired", "Your session has expired. Please log in again.")
    {
        HttpCode = HttpStatusCode.Unauthorized;
    }
}