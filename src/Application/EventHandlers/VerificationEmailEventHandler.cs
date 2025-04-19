/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService;

public class RegistrationEventHandler(
    IEmailService _emailService,
    ProjectSettings _projectSettings
) : INotificationHandler<RegistrationEvent>
{
    public Task Handle(RegistrationEvent notification, CancellationToken cancellationToken)
    {
        var user = notification.User;

        _emailService.SendRegistrationEmailAsync(
            user.EmailAddress.Value,
            _projectSettings.VerificationLink
        );

        return Task.CompletedTask;
    }
}