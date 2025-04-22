/*
* Author: Steve Bang
* History:
* - [2025-04-20] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService;
using Steve.ManagerHero.UserService.Application.Interfaces.Caching;
using Steve.ManagerHero.UserService.Helpers;

public class VerificationEmailEventHandler(
    IEmailService _emailService,
    ProjectSettings _projectSettings,
    ITokenCache _tokenCache
) : INotificationHandler<EmailVerificationEvent>
{
    private const int ExpriresMinute = 60;

    public Task Handle(EmailVerificationEvent notification, CancellationToken cancellationToken)
    {
        var user = notification.User;

        string encryptToken = EncryptionAESHelper.EncryptObject<UserPayloadEncrypt>(
            new UserPayloadEncrypt(user.Id),
            EncryptionPurpose.VerificationEmailAddress.ToString()
        );

        // Build link
        UrlBuilder uriBuilder = new UrlBuilder(_projectSettings.VerificationLink)
        .AddQueryParameter("token", encryptToken);

        _emailService.SendVerificationEmailAsync(
            user.EmailAddress.Value,
            uriBuilder.ToString(),
            ExpriresMinute
        );

        _tokenCache.SetToken(encryptToken, ExpriresMinute);

        return Task.CompletedTask;
    }
}