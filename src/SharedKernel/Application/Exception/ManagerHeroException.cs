/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using System.Net;

namespace Steve.ManagerHero.SharedKernel.Application.Exception;

public class ManagerHeroException : System.Exception
{
    public HttpStatusCode HttpCode { get; set; } = HttpStatusCode.InternalServerError;
    public string Code { get; set; }

    public ManagerHeroException(string code, string message) : base(message)
    {
        Code = code;
    }
}