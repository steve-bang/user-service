/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.SharedKernel.Application.Exception;

public class InvalidTokenException : UnauthorizedException
{
    public InvalidTokenException() : base(
        code: "InvalidToken",
        message: "The token is invalid.")
    {
    }
}