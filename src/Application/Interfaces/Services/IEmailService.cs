/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/


namespace Steve.ManagerHero.UserService.Application.Interfaces.Repository;

public interface IEmailService
{

    Task SendRegistrationEmailAsync(string email, string verificationLink);

    Task SendResetPasswordAsync(string email, string resetLink, int expiryMinutes);

    Task SendVerificationEmailAsync(string email, string verificationLink, int expiryMinutes);

}