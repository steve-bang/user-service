/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.Constants;

/// <summary>
/// Represents the type of social provider
/// </summary>
public enum SocialProviderType
{
    /// <summary>
    /// Google OAuth2 provider
    /// </summary>
    Google = 1,

    /// <summary>
    /// Facebook OAuth2 provider
    /// </summary>
    Facebook = 2,

    /// <summary>
    /// GitHub OAuth2 provider
    /// </summary>
    GitHub = 3,

    /// <summary>
    /// Microsoft OAuth2 provider
    /// </summary>
    Microsoft = 4,

    /// <summary>
    /// Apple OAuth2 provider
    /// </summary>
    Apple = 5,

    /// <summary>
    /// Twitter OAuth2 provider
    /// </summary>
    Twitter = 6,

    /// <summary>
    /// LinkedIn OAuth2 provider
    /// </summary>
    LinkedIn = 7
} 