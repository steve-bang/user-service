/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using System.Net;

namespace Steve.ManagerHero.UserService.Domain.Exceptions;
public class NotFoundDataException : ManagerHeroException
{
    public NotFoundDataException(string code, string messsage) : base( code, messsage)
    {
        HttpCode = HttpStatusCode.NotFound;
    }
}