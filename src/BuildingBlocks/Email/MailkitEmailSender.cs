
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Steve.ManagerHero.BuildingBlocks.Email.Options;

namespace Steve.ManagerHero.BuildingBlocks.Email;

public class MailkitEmailSender(
    IOptions<MailkitOptions> _config,
    ILogger<MailkitEmailSender> _logger
) : IEmailSender
{
    private readonly MailkitOptions _config = _config.Value;

    public async Task SendAsync(EmailRecipientOptions options)
    {
        try
        {

            using var client = new SmtpClient(_config.Host, _config.Port)
            {
                Credentials = new NetworkCredential(_config.UserName, _config.Password),
                EnableSsl = _config.EnableSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config.From, _config.DisplayName),
                Subject = options.Subject,
                Body = options.Body,
                IsBodyHtml = options.IsBodyHtml
            };

            // Add email to
            foreach (string emailTo in options.EmailTo)
            {
                mailMessage.To.Add(emailTo);
            }

            await client.SendMailAsync(mailMessage);

            _logger.LogInformation(
                "Email sent. From: {From}, To: {EmailTo}, Subject: {Subject}, Content: {Content}",
                _config.From,
                options.EmailTo,
                options.Subject,
                options.Body
            );
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Error while trying to send an email to {To} with the subject is {Subject}", options.EmailTo, options.Subject);
            _logger.LogWarning(ex, "Error while trying to send an email detail");
        }
    }
}