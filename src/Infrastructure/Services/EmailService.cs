/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using System.Dynamic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Steve.ManagerHero.UserService.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _smtpSettings;
    private readonly ProjectSettings _projectSettings;

    public EmailService(
        SmtpSettings smtpSettings,
        ProjectSettings projectSettings
    )
    {
        _smtpSettings = smtpSettings;
        _projectSettings = projectSettings;
    }

    public async Task SendEmailAsync(string to, string subject, string templateContent, ExpandoObject data)
    {
        var body = ReplaceTemplatePlaceholders(templateContent, data);

        using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
        {
            Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
            EnableSsl = _smtpSettings.EnableSsl
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_smtpSettings.FromEmail, _smtpSettings.FromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        mailMessage.To.Add(to);

        await client.SendMailAsync(mailMessage);
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
        var template = await File.ReadAllTextAsync("resources/EmailTemplates/registration.html");

        dynamic data = new ExpandoObject();
        data.VerificationLink = verificationLink;
        data.ProjectName = _projectSettings.Name;
        string title = $"{_projectSettings.Name} - Verify Your Email Address";

        await SendEmailAsync(
            email,
            title,
            template,
            data
        );
    }

    public async Task SendRestPasswordAsync(string email, string resetLink, int expiryMinutes)
    {
        var template = await File.ReadAllTextAsync("resources/EmailTemplates/reset-password.html");

        dynamic data = new ExpandoObject();
        data.ResetLink = resetLink;
        data.ExpiryMinutes = expiryMinutes;
        string title = $"{_projectSettings.Name} - Password Reset";

        await SendEmailAsync(
            email,
            title,
            template,
            data
        );
    }
}
