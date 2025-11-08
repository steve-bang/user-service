/*
* Author: Steve Bang
* Description: Middleware to restrict access to the API based on the IP address
* History:
* - [2025-05-10] - Created by mrsteve.bang@gmail.com
*/

using Serilog.Context;

namespace Steve.ManagerHero.Middlewares;

public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;


    public CorrelationIdMiddleware(
        RequestDelegate next
    )
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Request.Headers[ContextKeys.CorrelationId].FirstOrDefault() ?? Guid.NewGuid().ToString();

        // Set it in the response header for clients
        context.Response.Headers[ContextKeys.CorrelationId] = correlationId;

        // Push to Serilog context
        LogContext.PushProperty("CorrelationId", correlationId);

        await _next(context);
    }
}