
namespace Steve.ManagerHero.BuildingBlocks.Email.Options;

public class EmailRecipientOptions
{
    public string[] EmailTo { get; set; } = null!;

    public string[]? EmailCc { get; set; }

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public bool IsBodyHtml { get; set; }

    public EmailRecipientOptions(
        string[] emailTo,
        string[]? emailCc,
        string subject,
        string body,
        bool isBodyHtml = true
    )
    {
        EmailTo = emailTo;
        EmailCc = emailCc;
        Subject = subject;
        Body = body;
        IsBodyHtml = isBodyHtml;
    }


    public EmailRecipientOptions(
        string emailTo,
        string subject,
        string body,
        bool isBodyHtml = true
    ) : this([emailTo], null, subject, body, isBodyHtml)
    {
    }
}