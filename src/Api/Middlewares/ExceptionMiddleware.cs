/*
* Author: Steve Bang
* Description: Middleware to handle exceptions in the API
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/


using Steve.ManagerHero.Api.Models;
using Steve.ManagerHero.SharedKernel.Application.Response;

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
        ManagerHeroException? managerHeroException = exception as ManagerHeroException;

        // Build api response error custom
        ApiError apiResponse = ApiError.BuildException(exception);

        // Build with Exception
        context.Response.StatusCode =
        (int)(managerHeroException != null ? managerHeroException.HttpCode : System.Net.HttpStatusCode.InternalServerError);

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