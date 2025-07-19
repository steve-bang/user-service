/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.SharedKernel.Application.Response;

public class ApiError
{
    public string Code { get; set; } = null!;
    public string Message { get; set; } = null!;

    public const string DefaultMessage = "The request was failure.";

    public ApiError(string code, string message)
    {
        Code = code;
        Message = message;
    }


    /// <Message>
    /// Builds a 500 Internal Server error with exception details
    /// </Message>
    /// <param name="exception"></param>
    /// <returns></returns>
    public static ApiError BuildException(System.Exception exception)
    {
        ManagerHeroException? managerHeroException = exception as ManagerHeroException;

        if (managerHeroException is null)
            return new ApiError("InternalServerError", exception.ToString());
        else
            return new ApiError(
                managerHeroException.Code,
                managerHeroException.Message
            );
    }


    /// <Message>
    /// Builds a 500 Internal Server error with exception details
    /// </Message>
    /// <param name="exception"></param>
    /// <returns></returns>
    public static ApiError BuildException(ManagerHeroException exception)
    {

        return new ApiError(
                 exception.Code,
                 exception.Message
             );
    }
}