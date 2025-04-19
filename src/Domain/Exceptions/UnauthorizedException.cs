/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using System.Net;

namespace Steve.ManagerHero.UserService.Domain.Exceptions;
public class UnauthorizedException : ManagerHeroException
{
    public UnauthorizedException() : base("user.unauthorized", "User is not authenticated.")
    {
        HttpCode = HttpStatusCode.Unauthorized;
    }

        public UnauthorizedException(string message) : base("user.unauthorized", message)
    {
        HttpCode = HttpStatusCode.Unauthorized;
    }
}