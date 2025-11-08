
namespace Steve.ManagerHero.BuildingBlocks.Email.Options;

public abstract class EmailOptions
{
    public string From { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public EmailOptions() { }

    public EmailOptions(string from, string displayName)
    {
        From = from;
        DisplayName = displayName;
    }
}