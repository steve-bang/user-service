/*
* Author: Steve Bang
* History:
* - [2025-04-19] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Domain.AggregatesModel;

public class UserIdentity : AggregateRoot
{
    /// <summary>
    /// The identity provider.
    /// </summary>
    public IdentityProviderType Type { get; private set; }

    /// <summary>
    /// The id of the user.
    /// </summary>
    public Guid UserId { get; private set; }

    /// <summary>
    /// The provider id returned by the provider.
    /// If the provider is an OAuth provider, the id refers to the user's account with the OAuth provider. If the provider is email or phone, the id is the user's id from the User table.
    /// </summary>
    public string ProviderId { get; private set; }

    /// <summary>
    /// The identity metadata. For OAuth and SAML identities, this contains information about the user from the provider.
    /// </summary>
    public IDictionary<string, object> IdentityData { get; private set; }

    /// <summary>
    /// The timestamp that the identity was last used to sign in.
    /// </summary>
    public DateTime LastLoginAt { get; private set; }

    /// <summary>
    /// The timestamp that the identity was last updated.
    /// </summary>
    public DateTime? UpdateAt { get; private set; }

    /// <summary>
    /// The timestamp that the identity was created.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    public UserIdentity() { }

    public UserIdentity(
        IdentityProviderType type,
        User user,
        string providerId,
        IDictionary<string, object> identityData
    )
    {
        Type = type;
        UserId = user.Id;
        
        // Set provider id by type
        if (type == IdentityProviderType.Email || type == IdentityProviderType.Phone)
            ProviderId = user.Id.ToString();
        else
            ProviderId = providerId;

        IdentityData = identityData;

        LastLoginAt = DateTime.UtcNow;
        CreatedAt = DateTime.UtcNow;
    }

}