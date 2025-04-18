/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Steve.ManagerHero.UserService.Infrastructure;

public class CartAppContextFactory : IDesignTimeDbContextFactory<UserAppContext>
{
    public UserAppContext CreateDbContext(string[] args)
    {
        // Get the environment name from the environment variable
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        // Build configuration
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Get the connection string
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Configure DbContextOptions
        var optionsBuilder = new DbContextOptionsBuilder<UserAppContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new UserAppContext(optionsBuilder.Options, null); // Pass null for IMediator since it's not needed for migrations
    }
}