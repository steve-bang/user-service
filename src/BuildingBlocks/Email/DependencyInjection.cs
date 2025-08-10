
using Microsoft.Extensions.DependencyInjection.Extensions;
using Steve.ManagerHero.BuildingBlocks.Email.Options;

namespace Steve.ManagerHero.BuildingBlocks.Email;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddEmailService(this IHostApplicationBuilder builder)
    {
        var smtpSetting = builder.Configuration.GetSection(nameof(EmailOptions));
        if (!smtpSetting.Exists())
        {
            throw new NotImplementedException($"{nameof(EmailOptions)} section is missing in the appsettings.json file.");
        }

        builder.Services.TryAddSingleton<IEmailSender, MailkitEmailSender>();

        builder.Services.AddOptions<MailkitOptions>()
            .Bind(smtpSetting)
            .ValidateOnStart();

        return builder;
    }
}