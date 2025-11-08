/*
* Author: Steve Bang
* Description: Middleware to handle logs request and response in the API
* History:
* - [2025-08-09] - Created by mrsteve.bang@gmail.com
*/


using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using Steve.ManagerHero.Application.Features.SystemLogs.Commands;
using Steve.ManagerHero.UserService.Domain.Entities;

namespace Steve.ManagerHero.Middlewares;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, IMediator mediator)
    {
        var stopwatch = Stopwatch.StartNew();

        context.Request.EnableBuffering();

        var requestBody = await ReadStreamAsync(context.Request.Body);
        context.Request.Body.Position = 0;

        var originalBodyStream = context.Response.Body;
        using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        await _next(context);

        stopwatch.Stop();

        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var responseBody = await ReadStreamAsync(responseBodyStream);
        responseBodyStream.Position = 0;
        await responseBodyStream.CopyToAsync(originalBodyStream);

        var logEntry = SystemLogEntity.Create(
            userId: !string.IsNullOrEmpty(userId) ? Guid.Parse(userId) : null,
            correlationId: context.Response.Headers[ContextKeys.CorrelationId],
            httpMethod: context.Request.Method,
            path: context.Request.Path,
            statusCode: context.Response.StatusCode,
            durationMs: stopwatch.ElapsedMilliseconds,
            requestHeaders: SerializeHeaders(context.Request.Headers),
            responseHeaders: SerializeHeaders(context.Response.Headers),
            requestBodySnippet: requestBody,
            responseBodySnippet: responseBody,
            ipAddress: context.Connection.RemoteIpAddress?.ToString(),
            userAgent: context.Request.Headers["User-Agent"].ToString()
        );

        CreateSystemLogCommand createSystemLogCommand = new CreateSystemLogCommand(logEntry);
        await mediator.Send(createSystemLogCommand);
    }

    private async Task<string> ReadStreamAsync(Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);
        var text = await reader.ReadToEndAsync();
        stream.Seek(0, SeekOrigin.Begin);
        return text;
    }

    private string SerializeHeaders(IHeaderDictionary headers)
    {
        return JsonConvert.SerializeObject(headers);
    }
}

