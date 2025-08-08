/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using System.Net;

namespace Steve.ManagerHero.SharedKernel.Application.Exception;

public class UnauthorizedException : ManagerHeroException
{
    public UnauthorizedException(string code, string message) : base(code, message)
    {
        HttpCode = HttpStatusCode.Unauthorized;
    }
    
    public UnauthorizedException() : this("Unauthorized", "User is not authenticated.")
    {
    }

    public UnauthorizedException(string message) : this("Unauthorized", message)
    {
    }

}