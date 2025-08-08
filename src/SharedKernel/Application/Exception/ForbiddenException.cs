/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using System.Net;

namespace Steve.ManagerHero.SharedKernel.Application.Exception;

public class ForbiddenException : ManagerHeroException
{
    public ForbiddenException(string code, string message) : base(code, message)
    {
        HttpCode = HttpStatusCode.Forbidden;
    }

    public ForbiddenException() : this("Forbidden", "You do not have permission to perform this action.")
    {

    }

    public ForbiddenException(string message) : this("Forbidden", message)
    {

    }
}