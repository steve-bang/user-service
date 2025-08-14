
using Steve.ManagerHero.BuildingBlocks.Policy.Otp;

namespace Steve.ManagerHero.BuildingBlocks.Security.Otp;

public static class DependencyInjection
{
    /// <summary>
    /// Add the authentication method to the application
    /// </summary>
    /// <param name="builder">The host builder</param>
    /// <exception cref="NotImplementedException"></exception>
    public static IHostApplicationBuilder AddPolicyService(this IHostApplicationBuilder builder)
    {
        // Add otp policy options
        var otpPolicyOptions = builder.Configuration.GetSection("Security:OtpPolicy");

        builder.Services.AddOptions<OtpPolicyOptions>()
            .Bind(otpPolicyOptions)
            .ValidateOnStart();

        return builder;
    }
}