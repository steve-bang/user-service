/*
* Author: Steve Bang
* Description: Middleware to handle exceptions in the API
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/


using Steve.ManagerHero.Api.Models;

namespace Steve.ManagerHero.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            WriteLogging(ex);
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Build api response error custom
        ApiResponseError apiResponse = ApiResponseError.BuildException(exception);

        // Build with Exception
        context.Response.StatusCode = apiResponse.StatusCode;

        // Write and response
        return context.Response.WriteAsJsonAsync(apiResponse);
    }

    private void WriteLogging(Exception exception)
    {
        ManagerHeroException? managerHeroException = exception as ManagerHeroException;

        if (managerHeroException != null)
        {
            _logger.LogError(managerHeroException.Message);
        }
        else
        {
            _logger.LogError(exception, "Unhandled exception");
        }
    }
}