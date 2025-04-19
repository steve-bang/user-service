/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Steve.ManagerHero.UserService.Application.Auth;
using Steve.ManagerHero.UserService.Domain.Exceptions;
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
                options.Authority = jwtSettingsValue.Issuer;
                options.Audience = jwtSettingsValue.Audience;
                options.RequireHttpsMetadata = false;

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

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        throw new UnauthorizedException();
                    }
                };
            });

        builder.Services.AddAuthorization();
    }
}