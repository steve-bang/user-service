/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using System.Net;

namespace Steve.ManagerHero.SharedKernel.Application.Exception;

public class ConflictException : ManagerHeroException
{
    public ConflictException(string code, string messsage) : base( code, messsage)
    {
        HttpCode = HttpStatusCode.Conflict;
    }
}