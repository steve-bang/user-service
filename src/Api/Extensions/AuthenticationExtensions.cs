/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Steve.ManagerHero.Application.Features.Sessions.Queries;
using Steve.ManagerHero.BuildingBlocks.Authentication;
using Steve.ManagerHero.UserService.Application.Auth;
using Steve.ManagerHero.UserService.Infrastructure.Auth;

namespace Steve.ManagerHero.UserService.Extensions;

public static class AuthenticationExtensions
{
    /// <summary>
    /// Add the authentication method to the application
    /// </summary>
    /// <param name="builder">The host builder</param>
    /// <exception cref="NotImplementedException"></exception>
    public static void AddAuthenticationService(this IHostApplicationBuilder builder)
    {
        // Add the JWT settings
        var jwtSettings = builder.Configuration.GetSection("Jwt");
        if (!jwtSettings.Exists())
        {
            throw new NotImplementedException("JwtSettings section is missing in the appsettings.json file.");
        }

        var jwtSettingsValue = jwtSettings.Get<JwtSettings>();

        if (jwtSettingsValue == null)
        {
            throw new NotImplementedException("JwtSettings section is missing in the appsettings.json file.");
        }
        builder.Services.AddSingleton(jwtSettingsValue);

        // Add the JWT handler
        builder.Services.AddScoped<IJwtHandler, JwtHandler>();


        // Add JWT handler
        builder.Services
            .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(
            options =>
            {
                // Basic JWT settings
                options.Authority = jwtSettingsValue.Issuer;
                options.Audience = jwtSettingsValue.Audience;
                options.RequireHttpsMetadata = false;

                // Token validation parameters (signature, issuer, lifetime, etc.)
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettingsValue.Issuer,
                    ValidAudience = jwtSettingsValue.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettingsValue.Secret)),
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