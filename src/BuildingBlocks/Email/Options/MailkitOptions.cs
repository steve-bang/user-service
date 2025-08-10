
namespace Steve.ManagerHero.BuildingBlocks.Email.Options;

public class MailkitOptions : EmailOptions
{
    public string Host { get; init; } = null!;
    public int Port { get; init; }
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
    public bool EnableSsl { get; set; }

    public MailkitOptions() : base() { }

    public MailkitOptions(
        string from,
        string displayName,
        string host,
        int port,
        string username,
        string password,
        bool enableSsl
    ) : base(from, displayName)
    {
        Host = host;
        Port = port;
        UserName = username;
        Password = password;
        EnableSsl = enableSsl;
    }
}