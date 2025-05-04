/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using System.Net;

namespace Steve.ManagerHero.UserService.Domain.Exceptions;
public class ForbiddenException : ManagerHeroException
{
    public ForbiddenException() : base("user.forbidden", "You do not have permission to perform this action.")
    {
        HttpCode = HttpStatusCode.Forbidden;
    }

    public ForbiddenException(string message) : base("user.forbidden", message)
    {
        HttpCode = HttpStatusCode.Forbidden;
    }
}