/*
* Author: Steve Bang
* History:
* - [2025-04-18] - Created by mrsteve.bang@gmail.com
*/

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

        // builder.Services.AddScoped<IIdentityService, IdentityService>();

        // builder.AddDefaultAuthentication();
    }
}