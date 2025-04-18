/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using System.Net;

namespace Steve.ManagerHero.UserService.Domain.Exceptions;
public class NotFoundDataException : ManagerHeroException
{
    public NotFoundDataException(string entity, string code) : base(
        code,
        string.Format("The {0} was not found.", entity)
    )
    {
        HttpCode = HttpStatusCode.NotFound;
    }
}