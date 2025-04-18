/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using System.Net;

namespace Steve.ManagerHero.UserService.Domain.Exceptions;
public class ManagerHeroException : Exception
{
    public HttpStatusCode HttpCode { get; set; } = HttpStatusCode.InternalServerError;
    public string ErrorCode { get; set; }


    public ManagerHeroException(string errorCode, string message) : base(message)
    {
        ErrorCode = errorCode;
    }
}