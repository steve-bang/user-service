
using Microsoft.Extensions.DependencyInjection.Extensions;
using Steve.ManagerHero.BuildingBlocks.SMS.Options;

namespace Steve.ManagerHero.BuildingBlocks.SMS;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddSmsService(this IHostApplicationBuilder builder)
    {
        var smsOptions = builder.Configuration.GetSection("SmsOptions:Twilio");
        if (!smsOptions.Exists())
        {
            throw new NotImplementedException($"SmsOptions:Twilio section is missing in the appsettings.json file.");
        }

        builder.Services.TryAddSingleton<ISmsSender, TwilioSmsSender>();

        builder.Services.AddOptions<TwilioOptions>()
            .Bind(smsOptions)
            .ValidateOnStart();

        return builder;
    }
}