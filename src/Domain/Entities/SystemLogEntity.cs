
namespace Steve.ManagerHero.UserService.Domain.Entities;

public class SystemLogEntity : Entity
{

    // Core HTTP info
    public string HttpMethod { get; private set; } = null!;
    public string Path { get; private set; } = null!;
    public int StatusCode { get; private set; }
    public long DurationMs { get; private set; }

    // Optional context
    public Guid? UserId { get; private set; }
    public string? CorrelationId { get; private set; }
    public string? IpAddress { get; private set; }
    public string? UserAgent { get; private set; }

    // Additional details
    public string? RequestHeaders { get; private set; }
    public string? ResponseHeaders { get; private set; }
    public string? RequestBodySnippet { get; private set; }
    public string? ResponseBodySnippet { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }

    // Constructor for EF
    private SystemLogEntity() : base() { }

    private SystemLogEntity(
        string httpMethod,
        string path,
        int statusCode,
        long durationMs,
        Guid? userId,
        string? correlationId,
        string? ipAddress,
        string? userAgent,
        string? requestHeaders,
        string? responseHeaders,
        string? requestBodySnippet,
        string? responseBodySnippet
    )
    {
        HttpMethod = httpMethod;
        Path = path;
        StatusCode = statusCode;
        DurationMs = durationMs;
        UserId = userId;
        CorrelationId = correlationId;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        RequestHeaders = requestHeaders;
        ResponseHeaders = responseHeaders;
        RequestBodySnippet = requestBodySnippet;
        ResponseBodySnippet = responseBodySnippet;
        CreatedAt = DateTime.UtcNow;
    }

    public static SystemLogEntity Create(
        string httpMethod,
        string path,
        int statusCode,
        long durationMs,
        Guid? userId = null,
        string? correlationId = null,
        string? ipAddress = null,
        string? userAgent = null,
        string? requestHeaders = null,
        string? responseHeaders = null,
        string? requestBodySnippet = null,
        string? responseBodySnippet = null
    ) => new(
        httpMethod,
        path,
        statusCode,
        durationMs,
        userId,
        correlationId,
        ipAddress,
        userAgent,
        requestHeaders,
        responseHeaders,
        requestBodySnippet,
        responseBodySnippet
    );
}