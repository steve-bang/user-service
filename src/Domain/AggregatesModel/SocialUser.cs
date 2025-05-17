/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.AggregatesModel;

public class SocialUser : AggregateRoot
{
    public Guid ProviderId { get; private set; }

    public Guid UserId { get; private set; }

    public User? User { get; private set; }

    public string ProviderUserId { get; private set; }

    public string ProfileData { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public SocialUser() { }

    /// <summary>
    /// Initialize a new social user
    /// </summary>
    public SocialUser(
        Guid providerId,
        Guid userId,
        string providerUserId,
        string profileData
    )
    {
        ProviderId = providerId;
        UserId = userId;
        ProviderUserId = providerUserId;
        ProfileData = profileData;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Create a new social user
    /// </summary>
    public static SocialUser Create(
        Guid providerId,
        Guid userId,
        string providerUserId,
        string profileData
    )
    {
        return new SocialUser(
            providerId,
            userId,
            providerUserId,
            profileData
        );
    }
}