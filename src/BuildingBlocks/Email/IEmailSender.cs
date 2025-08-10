

using Steve.ManagerHero.BuildingBlocks.Email.Options;

namespace Steve.ManagerHero.BuildingBlocks.Email;

public interface IEmailSender
{
    Task SendAsync(EmailRecipientOptions options);
}