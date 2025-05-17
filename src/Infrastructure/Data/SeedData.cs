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

        // Seed Social Providers
        if (!context.SocialProviders.Any())
        {
            var socialProviders = new List<SocialProvider>
            {
                SocialProvider.Create(
                    "Google",
                    "Google",
                    SocialProviderType.Google,
                    "your-google-client-id",
                    "your-google-client-secret",
                    ["openid", "profile", "email"],
                    "https://accounts.google.com/o/oauth2/v2/auth",
                    "https://oauth2.googleapis.com/token",
                    "https://www.googleapis.com/oauth2/v3/userinfo",
                    "https://www.googleapis.com/oauth2/v3/certs",
                    "",
                    true,
                    true,
                    true
                ),
                SocialProvider.Create(
                    "Facebook",
                    "Facebook",
                    SocialProviderType.Facebook,
                    "your-facebook-client-id",
                    "your-facebook-client-secret",
                    ["email", "public_profile"],
                    "https://www.facebook.com/v18.0/dialog/oauth",
                    "https://graph.facebook.com/v18.0/oauth/access_token",
                    "https://graph.facebook.com/me",
                    "",
                    "",
                    true,
                    true,
                    true
                ),
                SocialProvider.Create(
                    "GitHub",
                    "GitHub",
                    SocialProviderType.GitHub,
                    "your-github-client-id",
                    "your-github-client-secret",
                    ["user:email", "read:user"],
                    "https://github.com/login/oauth/authorize",
                    "https://github.com/login/oauth/access_token",
                    "https://api.github.com/user",
                    "",
                    "",
                    true,
                    true,
                    true
                ),
                SocialProvider.Create(
                    "Microsoft",
                    "Microsoft",
                    SocialProviderType.Microsoft,
                    "your-microsoft-client-id",
                    "your-microsoft-client-secret",
                    ["openid", "profile", "email"],
                    "https://login.microsoftonline.com/common/oauth2/v2.0/authorize",
                    "https://login.microsoftonline.com/common/oauth2/v2.0/token",
                    "https://graph.microsoft.com/oidc/userinfo",
                    "https://login.microsoftonline.com/common/discovery/v2.0/keys",
                    "",
                    true,
                    true,
                    true
                ),
                SocialProvider.Create(
                    "Apple",
                    "Apple",
                    SocialProviderType.Apple,
                    "your-apple-client-id",
                    "your-apple-client-secret",
                    ["name", "email"],
                    "https://appleid.apple.com/auth/authorize",
                    "https://appleid.apple.com/auth/token",
                    "https://appleid.apple.com/auth/userinfo",
                    "",
                    "",
                    true,
                    true,
                    true
                )
            };
            context.SocialProviders.AddRange(socialProviders);
            context.SaveChanges();
        }

        // Seed Social Users
        if (!context.SocialUsers.Any())
        {
            var adminUser = context.Users.First(u => u.EmailAddress.Value == "admin@example.com");
            var regularUser = context.Users.First(u => u.EmailAddress.Value == "john.doe@example.com");
            var googleProvider = context.SocialProviders.First(p => p.Type == SocialProviderType.Google);
            var githubProvider = context.SocialProviders.First(p => p.Type == SocialProviderType.GitHub);

            var socialUsers = new List<SocialUser>
            {
                SocialUser.Create(
                    googleProvider.Id,
                    adminUser.Id,
                    "google-123",
                    JsonSerializer.Serialize(new
                    {
                        name = "Admin User",
                        email = "admin@example.com",
                        picture = "https://example.com/avatar1.jpg",
                        verified = true
                    })
                ),
                SocialUser.Create(
                    githubProvider.Id,
                    regularUser.Id,
                    "github-456",
                    JsonSerializer.Serialize(new
                    {
                        name = "John Doe",
                        email = "john.doe@example.com",
                        avatar_url = "https://example.com/avatar2.jpg",
                        verified = true
                    })
                )
            };
            context.SocialUsers.AddRange(socialUsers);
            context.SaveChanges();
        }
    }
}