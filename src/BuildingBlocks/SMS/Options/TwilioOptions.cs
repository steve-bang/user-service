
namespace Steve.ManagerHero.BuildingBlocks.SMS.Options;

public class TwilioOptions : SmsOptions
{
    public string From { get; set; } = null!;

    public string AccountSid { get; set; } = null!;

    public string AuthToken { get; set; } = null!;

    public TwilioOptions() : base() { }

    public TwilioOptions(
        string from,
        string accountSid,
        string authToken
    )
    {
        From = from;
        AccountSid = accountSid;
        AuthToken = authToken;
    }
}