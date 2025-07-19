using Microsoft.Extensions.DependencyInjection;
using Steve.ManagerHero.UserService.Domain.AggregatesModel;
using Steve.ManagerHero.UserService.Domain.Constants;
using Steve.ManagerHero.UserService.Domain.ValueObjects;
using Steve.ManagerHero.UserService.Infrastructure;
using System.Text.Json;

namespace Steve.ManagerHero.UserService.Infrastructure.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UserAppContext>();

        // Seed Roles
        if (!context.Roles.Any())
        {
            var roles = new List<Role>
            {
                new Role("Admin", "Administrator with full access"),
                new Role("User", "Regular user with limited access"),
                new Role("Manager", "Manager with elevated access")
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();
        }

        // Seed Permissions
        if (!context.Permissions.Any())
        {
            var permissions = new List<Permission>
            {
                new Permission("users.view", "View Users", "Can view users"),
                new Permission("users.create", "Create Users", "Can create users"),
                new Permission("users.edit", "Edit Users", "Can edit users"),
                new Permission("users.delete", "Delete Users", "Can delete users")
            };
            context.Permissions.AddRange(permissions);
            context.SaveChanges();
        }

        // Seed Users
        if (!context.Users.Any())
        {
            var adminRole = context.Roles.First(r => r.Name == "Admin");
            var userRole = context.Roles.First(r => r.Name == "User");

            var adminUser = User.Create(
                "Admin",
                "User",
                "admin@example.com",
                "Admin123!"
            );
            adminUser.AddRole(adminRole);

            var regularUser = User.Create(
                "John",
                "Doe",
                "john.doe@example.com",
                "User123!"
            );
            regularUser.AddRole(userRole);

            context.Users.AddRange(adminUser, regularUser);
            context.SaveChanges();
        }


        context.SaveChanges();
    }


}
