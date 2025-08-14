
using Steve.ManagerHero.BuildingBlocks.SMS.Options;

namespace Steve.ManagerHero.BuildingBlocks.SMS;

public interface ISmsSender
{
    Task SendAsync(SmsRecipientOptions options);
}