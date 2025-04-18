/*
* Author: Steve Bang
* History:
* - [2024-04-16] - Created by mrsteve.bang@gmail.com
*/

using System.Net;

namespace Steve.ManagerHero.UserService.Domain.Exceptions;
public class BadRequestException : ManagerHeroException
{
    public BadRequestException(string code, string messsage) : base( code, messsage)
    {
        HttpCode = HttpStatusCode.BadRequest;
    }
}