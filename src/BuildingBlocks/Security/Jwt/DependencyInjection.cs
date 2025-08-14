using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Steve.ManagerHero.Application.Features.Sessions.Queries;

namespace Steve.ManagerHero.BuildingBlocks.Security.Jwt;

public static class DependencyInjection
{
    /// <summary>
    /// Add the authentication method to the application
    /// </summary>
    /// <param name="builder">The host builder</param>
    /// <exception cref="NotImplementedException"></exception>
    public static void AddJwtAuthService(this IHostApplicationBuilder builder)
    {
        // Add the JWT settings
        var jwtOptions = builder.Configuration.GetSection("Jwt");
        if (!jwtOptions.Exists())
        {
            throw new NotImplementedException("JwtSettings section is missing in the appsettings.json file.");
        }

        var jwtOptionsValue = jwtOptions.Get<JwtOptions>();

        if (jwtOptionsValue == null)
        {
            throw new NotImplementedException("JwtSettings section is missing in the appsettings.json file.");
        }
        builder.Services.AddSingleton(jwtOptionsValue);

        // Add the JWT handler
        builder.Services.AddScoped<IJwtHandler, JwtHandler>();


        // Add JWT handler
        builder.Services
            .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
            options =>
            {
                // Basic JWT settings
                options.Authority = jwtOptionsValue.Issuer;
                options.Audience = jwtOptionsValue.Audience;
                options.RequireHttpsMetadata = false;

                // Token validation parameters (signature, issuer, lifetime, etc.)
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptionsValue.Issuer,
                    ValidAudience = jwtOptionsValue.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptionsValue.Secret)),
                };

                // JWT Event Handlers
                options.Events = new JwtBearerEvents
                {

                    // 🔒 Handle when authentication fails (e.g. token is expired)
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            throw new TokenExpiredException();
                        }

                        return Task.CompletedTask;
                    },

                    // 🔐 Runs when token is successfully validated (signature, expiry, etc.)
                    OnTokenValidated = async context =>
                    {

                        var accessToken = context.Request.Headers["Authorization"]
                                .FirstOrDefault()?
                                .Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);

                        if (string.IsNullOrEmpty(accessToken))
                        {
                            context.Fail("Access token is missing.");
                            return;
                        }

                        var sessionRepository = context.HttpContext.RequestServices.GetRequiredService<ISessionRepository>();
                        var jwtHandler = context.HttpContext.RequestServices.GetRequiredService<IJwtHandler>();
                        var mediator = context.HttpContext.RequestServices.GetRequiredService<IMediator>();

                        var jwtToken = context.SecurityToken as JwtSecurityToken;
                        var sessionId = jwtHandler.ExtraSessionId(accessToken);
                        var userId = jwtHandler.ExtraUserId(accessToken);

                        var session = await mediator.Send(new GetSessionByIdQuery(sessionId));

                        if (session.IsRevoked)
                            throw new SessionRevokedException();

                        // Optionally: You can attach extra claims or data to HttpContext here
                    },

                    // 🚫 Triggered when authentication is required but not provided or invalid
                    OnChallenge = context =>
                    {
                        throw new UnauthorizedException();
                    },

                    // Triggered when request resource was access deny.
                    OnForbidden = context =>
                    {
                        throw new ForbiddenException();
                    },
                };
            });

        builder.Services.AddAuthorization();
    }
}