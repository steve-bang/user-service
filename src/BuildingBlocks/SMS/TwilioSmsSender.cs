
using Microsoft.Extensions.Options;
using Steve.ManagerHero.BuildingBlocks.SMS.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Steve.ManagerHero.BuildingBlocks.SMS;

public class TwilioSmsSender(
        IOptions<TwilioOptions> _config,
    ILogger<TwilioSmsSender> _logger
) : ISmsSender
{
    private readonly TwilioOptions _config = _config.Value;

    private const string PlusCharacter = "+";

    public async Task SendAsync(SmsRecipientOptions options)
    {
        try
        {
            TwilioClient.Init(_config.AccountSid, _config.AuthToken);

            // Make sure the phone number must be has prefix + charactir. ex: +123456789
            if (!_config.From.StartsWith(PlusCharacter))
                _config.From = PlusCharacter + _config.From;

            if (!options.To.StartsWith(PlusCharacter))
                options.To = PlusCharacter + options.To;

            var result = await MessageResource.CreateAsync(
                    body: options.Message,
                    from: new Twilio.Types.PhoneNumber(_config.From),
                    to: new Twilio.Types.PhoneNumber(options.To)
            );

            _logger.LogInformation("SMS sent. From: {From}, To: {EmailTo}, Message: {Message}",
                _config.From,
                options.To,
                options.Message
            );
        }
        catch (Exception ex)
        {
            _logger.LogError("Error sent sms message. From: {From}, To: {EmailTo}, Message: {Message}",
                _config.From,
                options.To,
                options.Message
            );

            _logger.LogError(ex, "Error send sms message detail.");
        }
    }
}