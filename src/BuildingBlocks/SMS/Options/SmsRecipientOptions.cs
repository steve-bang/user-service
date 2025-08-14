
namespace Steve.ManagerHero.BuildingBlocks.SMS.Options;

public class SmsRecipientOptions
{
    public string To { get; set; } = null!;

    public string Message { get; set; } = null!;

    public SmsRecipientOptions(
        string phoneNumberTo,
        string content
    )
    {
        To = phoneNumberTo;
        Message = content;
    }
}