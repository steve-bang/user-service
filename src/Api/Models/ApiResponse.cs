/*
* Author: Steve Bang
* History:
* - [2025-04-16] - Created by mrsteve.bang@gmail.com
*/

using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Steve.ManagerHero.Api.Models;

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

/// <typeparam name="T">The type of data that is being returned</typeparam>
public class ApiResponseSuccess<T> : ApiResponse
{
    public T? Data { get; set; }

    public const string DefaultMessage = "The request was success.";

    /// <summary>
    /// Initializes a new instance of the ApiResponse class with the status code and data provided.
    /// </summary>
    /// <param name="statusCode">The status code of the response</param>
    /// <param name="data">The data to be returned</param>
    public ApiResponseSuccess(int statusCode, T? data) : base(true, statusCode, DefaultMessage)
    {
        Data = data;
    }

    /// <summary>
    /// Initializes a new instance of the ApiResponse class with the status code and data provided.
    /// </summary>
    /// <param name="statusCode">The status code of the response. The <see cref="HttpStatusCode"/> enum is used to provide the status code</param>
    /// <param name="data">The data to be returned</param>
    public ApiResponseSuccess(HttpStatusCode statusCode, T? data) : base(true, (int)statusCode, DefaultMessage)
    {
        Data = data;
    }



    /// <summary>
    /// Returns a response with a status code of 200 (OK) and the data provided
    /// </summary>
    /// <param name="data">The data to be returned</param>
    /// <returns></returns>
    public static ApiResponseSuccess<T> BuildOK(T data)
    {
        return new ApiResponseSuccess<T>(HttpStatusCode.OK, data);
    }

    /// <summary>
    /// Returns a response with a status code of OK (200) and the data provided
    /// </summary>
    /// <param name="data">The data to be returned</param>
    /// <returns></returns>
    public static ObjectResult BuildOKObjectResult(T data)
    {
        return new ObjectResult(new ApiResponseSuccess<T>(HttpStatusCode.OK, data))
        {
            StatusCode = (int)HttpStatusCode.OK
        };
    }

    /// <summary>
    /// Returns a response with a status code of Created (201) and the data provided
    /// </summary>
    /// <param name="data">The data to be returned</param>
    /// <returns></returns>
    public static ApiResponseSuccess<T> BuildCreated(T data)
    {
        return new ApiResponseSuccess<T>(HttpStatusCode.Created, data);
    }

    /// <summary>
    /// Returns a response with a status code of Created (201) and the data provided
    /// </summary>
    /// <param name="data">The data to be returned</param>
    /// <returns></returns>
    public static ObjectResult BuildCreatedObjectResult(T data)
    {
        return new ObjectResult(new ApiResponseSuccess<T>(HttpStatusCode.Created, data))
        {
            StatusCode = (int)HttpStatusCode.Created
        };
    }


    /// <summary>
    /// This method returns a response with a status code of 204 (No Content) and no data
    /// </summary>
    /// <returns></returns>
    public static NoContentResult BuildNoContent()
    {
        return new NoContentResult();
    }
}
