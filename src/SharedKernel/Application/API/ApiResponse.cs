/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.SharedKernel.Application.Response;

/// <summary>
/// The ApiResponse class is a generic class that is used to return responses from the API
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// Indicates whether the API request was successful or not
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// HTTP status code of the response
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Custom message describing the result
    /// </summary>
    public string Message { get; set; } = null!;

    public ApiResponse(bool success, int statusCode, string message)
    {
        Success = success;
        StatusCode = statusCode;
        Message = message;
    }

}
