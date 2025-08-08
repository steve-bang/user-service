
namespace Steve.ManagerHero.UserService.Infrastructure.Auth.External;

public class OAuthUser
{
    public string Id { get; set; }

    public string Email { get; set; }

    public string Name { get; set; }

    public string? PictureUrl { get; set; }

    public bool VerifiedAccount { get; set; }

    public OAuthUser(
        string id,
        string email,
        string name,
        bool verifiedAccount,
        string? pictureUrl = null
    )
    {
        Id = id;
        Email = email;
        Name = name;
        VerifiedAccount = verifiedAccount;
        PictureUrl = pictureUrl;
    }
}