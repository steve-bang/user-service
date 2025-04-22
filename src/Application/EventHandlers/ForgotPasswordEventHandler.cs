/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService;
using Steve.ManagerHero.UserService.Application.Interfaces.Caching;
using Steve.ManagerHero.UserService.Domain.Events;
using Steve.ManagerHero.UserService.Helpers;

public class ForgotPasswordEventHandler(
    IEmailService _emailService,
    ProjectSettings _projectSettings,
    ITokenCache _tokenCache
) : INotificationHandler<ForgotPasswordEvent>
{
    private const int OneHourExprired = 60;

    public Task Handle(ForgotPasswordEvent notification, CancellationToken cancellationToken)
    {
        var user = notification.User;

        string encryptToken = EncryptionAESHelper.EncryptObject<UserPayloadEncrypt>(
            new UserPayloadEncrypt(user.Id),
            EncryptionPurpose.ResetPassword.ToString()
        );

        // Build link
        UrlBuilder uriBuilder = new UrlBuilder(_projectSettings.ResetPassword)
            .AddQueryParameter("user-id", user.Id.ToString())
            .AddQueryParameter("token", encryptToken);

        _emailService.SendResetPasswordAsync(
            user.EmailAddress.Value,
            uriBuilder.ToString(),
            OneHourExprired
        );

        _tokenCache.SetToken(encryptToken, OneHourExprired);

        return Task.CompletedTask;
    }
}