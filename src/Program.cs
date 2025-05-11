
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Steve.ManagerHero.UserService.Extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{

    // Add services to the container.
    builder.Services.AddOpenApi();
    builder.Services.AddControllers();

    builder.Services.AddAutoMapper(typeof(Program));
    builder.AddCoreServices()
            .AddAuthenticationService();

    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

    // Add rate limiting settings
    builder.AddRateLimitSettings();

    builder.Host.UseSerilog((context, loggerConfiguration) =>
        loggerConfiguration
            .ReadFrom.Configuration(context.Configuration)
            .Enrich.FromLogContext()
    );

    var app = builder.Build();

    // Logs some information about the app
    Log.Information("Environment: {Environment}", app.Environment.EnvironmentName);
    Log.Information("Application Name: {ApplicationName}", app.Environment.ApplicationName);

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseForwardedHeaders(new ForwardedHeadersOptions
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    app.MapControllers();

    app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        var request = httpContext.Request;
        var user = httpContext.User?.Identity?.IsAuthenticated == true
            ? httpContext.User.Identity.Name
            : "Anonymous";

        var ipAddress = httpContext.Connection.RemoteIpAddress?.ToString();
        var userAgent = request.Headers["User-Agent"].FirstOrDefault() ?? "Unknown";

        var correlationId = request.Headers["X-Correlation-ID"].FirstOrDefault()
                         ?? httpContext.Response.Headers["X-Correlation-ID"].FirstOrDefault()
                         ?? "N/A";

        diagnosticContext.Set("CorrelationId", correlationId);
        diagnosticContext.Set("IPAddress", ipAddress ?? "Unknown");
        diagnosticContext.Set("User", user ?? "Anonymous");
        diagnosticContext.Set("UserAgent", userAgent);
    };
});

    // Config Pipeline middlewares
    // We need to add the middlewares in the order of execution
    app.ConfigPipelineMiddlewares();

    app.UseRateLimiter();

    app.UseAuthentication();
    app.UseAuthorization();

    Log.Information("[x] The app running...");

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "[x] Server terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}


