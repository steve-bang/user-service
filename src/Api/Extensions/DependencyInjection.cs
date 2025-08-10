/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.EntityFrameworkCore;
using Steve.ManagerHero.UserService.Infrastructure;
using Steve.ManagerHero.UserService.Infrastructure.Repository;
using FluentValidation;
using Steve.ManagerHero.Application.Features.Users.Commands;
using Steve.ManagerHero.UserService.Application.Auth;
using Steve.ManagerHero.UserService.Infrastructure.Services;
using Steve.ManagerHero.UserService.Application.Interfaces.Caching;
using Steve.ManagerHero.UserService.Infrastructure.Caching;
using Steve.ManagerHero.Application.Processors;
using Steve.ManagerHero.Application.Features.Permissions.Commands;
using Steve.ManagerHero.UserService.Application.Service;
using Steve.ManagerHero.UserService.Application.Interfaces.Services;
using Steve.ManagerHero.UserService.Infrastructure.Auth.External;
using Steve.ManagerHero.UserService.Infrastructure.Security;
using Steve.ManagerHero.UserService.Domain.Services;
using Steve.ManagerHero.BuildingBlocks.Email;

namespace Steve.ManagerHero.UserService.Extensions;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddCoreServices(this IHostApplicationBuilder builder)
    {
        // Add project settings
        builder.AddProjectSettings();

        // Add http context
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IIdentityService, IdentityService>();

        builder.Services.AddDbContext<UserAppContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Information)
        );

        builder.EnrichNpgsqlDbContext<UserAppContext>();

        // Add the migration service. When the application starts, it will check if the database is up to date. 
        // If not, it will run the migration to update the database.
        builder.Services.AddMigration<UserAppContext>();

        // Add the MediatR services
        builder.Services.AddMediatR(config =>
        {
            // Register all the handlers from the current assembly
            config.RegisterServicesFromAssemblyContaining<Program>();

            // Register the ValidationBehavior
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });

        // Register validator
        builder.Services.AddValidatorsFromAssemblyContaining<AssignPermissionToRoleCommandValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<ChangePasswordCommandValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<CreatePermissionCommandValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<ResetPasswordCommandValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<RemovePermissionsFromRoleCommandValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateUserCommandValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdatePermissionCommandValidator>();

        builder.Services.AddScoped<IGoogleAuthService, GoogleAuthService>();
        builder.Services.AddScoped<ExternalAuthServiceFactory>();
        builder.Services.AddScoped<IPermissionService, PermissionService>();


        builder.Services.AddScoped<IPasswordHasher, Pbkdf2PasswordHasher>();
        builder.Services.AddScoped<IPasswordPolicy, PasswordPolicy>();
        builder.Services.AddScoped<IPasswordHistoryPolicyService, PasswordHistoryPolicyService>();

        builder.Services.AddScoped<IEmailService, EmailService>();

        // Register smtp setting
        builder.AddEmailService();

        // Add caching
        builder.AddCacheServices();

        // Register repositories
        builder.AddRepositories();

        // Add SCIM filter processor
        builder.Services.AddScoped<IScimFilterProcessor<Permission>, ScimFilterProcessor<Permission>>();
        builder.Services.AddScoped<IScimFilterProcessor<Role>, ScimFilterProcessor<Role>>();
        builder.Services.AddScoped<IScimFilterProcessor<User>, ScimFilterProcessor<User>>();

        return builder;
    }

    public static IHostApplicationBuilder AddRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ISessionRepository, SessionRepository>();

        return builder;
    }

    public static IHostApplicationBuilder AddProjectSettings(this IHostApplicationBuilder builder)
    {
        var projectSetting = builder.Configuration.GetSection(nameof(ProjectSettings));
        if (!projectSetting.Exists())
        {
            throw new NotImplementedException("ProjectSettings section is missing in the appsettings.json file.");
        }

        var projectSettingValue = projectSetting.Get<ProjectSettings>();

        if (projectSettingValue == null)
        {
            throw new NotImplementedException("ProjectSettings section is missing in the appsettings.json file.");
        }
        builder.Services.AddSingleton(projectSettingValue);

        return builder;
    }

    public static IHostApplicationBuilder AddCacheServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();

        // Add caching service
        builder.Services.AddScoped<ITokenCache, TokenCache>();
        builder.Services.AddScoped<IUserCache, UserCache>();
        builder.Services.AddScoped<ISessionCache, SessionCache>();
        builder.Services.AddScoped<IPermissionCache, PermissionCache>();

        return builder;
    }


}