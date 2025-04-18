
using Microsoft.EntityFrameworkCore;
using Steve.ManagerHero.UserService.Infrastructure;
using MediatR;
using Steve.ManagerHero.UserService.Application.Interfaces.Repository;
using Steve.ManagerHero.UserService.Infrastructure.Repository;
using FluentValidation;
using Steve.ManagerHero.Application.Features.Users.Commands;

namespace Steve.ManagerHero.UserService.Extensions;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddCoreServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<UserAppContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
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
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();


        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        return builder;
    }

}