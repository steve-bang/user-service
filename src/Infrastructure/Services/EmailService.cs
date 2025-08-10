/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using System.Dynamic;
using System.Text;
using Steve.ManagerHero.BuildingBlocks.Email;

namespace Steve.ManagerHero.UserService.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IEmailSender _emailSender;
    private readonly ProjectSettings _projectSettings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IEmailSender emailSender,
        ProjectSettings projectSettings,
        ILogger<EmailService> logger
    )
    {
        _emailSender = emailSender;
        _projectSettings = projectSettings;
        _logger = logger;
    }


    private string ReplaceTemplatePlaceholders(string template, ExpandoObject data)
    {
        var sb = new StringBuilder(template);
        var dict = (IDictionary<string, object>)data;

        foreach (var kvp in dict)
        {
            sb.Replace($"{{{{{kvp.Key}}}}}", kvp.Value?.ToString() ?? "");
        }

        return sb.ToString();
    }


    public async Task SendRegistrationEmailAsync(string email, string verificationLink)
    {
        try
        {
            var template = await File.ReadAllTextAsync("Api/resources/EmailTemplates/registration.html");

            dynamic data = new ExpandoObject();
            data.VerificationLink = verificationLink;
            data.ProjectName = _projectSettings.Name;
            string subject = $"{_projectSettings.Name} - Verify Your Email Address";

            await SendAsync(
                emailTo: email,
                subject: subject,
                data: data,
                template: template
            );
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error while trying to send registration email async");
        }
    }

    public async Task SendResetPasswordAsync(string email, string resetLink, int expiryMinutes)
    {
        var template = await File.ReadAllTextAsync("resources/EmailTemplates/reset-password.html");

        dynamic data = new ExpandoObject();
        data.ResetLink = resetLink;
        data.ExpiryMinutes = expiryMinutes;
        string subject = $"{_projectSettings.Name} - Password Reset";

        await SendAsync(
            emailTo: email,
            subject: subject,
            data: data,
            template: template
        );
    }

    public async Task SendVerificationEmailAsync(string email, string verificationLink, int expiryMinutes)
    {
        var template = await File.ReadAllTextAsync("resources/EmailTemplates/email-verification.html");

        dynamic data = new ExpandoObject();
        data.VertificationLink = verificationLink;
        data.ExpiryMinutes = expiryMinutes;
        string subject = $"{_projectSettings.Name} - Verify Your Email";

        await SendAsync(
            emailTo: email,
            subject: subject,
            data: data,
            template: template
        );
    }

    private async Task SendAsync(
        string emailTo,
        string subject,
        ExpandoObject data,
        string template
    )
    {
        var body = ReplaceTemplatePlaceholders(template, data);

        await _emailSender.SendAsync(
            new BuildingBlocks.Email.Options.EmailRecipientOptions(
                emailTo: emailTo,
                subject: subject,
                body: body
            )
        );
    }
}
