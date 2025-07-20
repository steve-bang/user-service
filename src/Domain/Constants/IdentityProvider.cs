/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.Constants;

/// <summary>
/// Represents the identity provider
/// </summary>
public enum IdentityProvider
{
    /// <summary>
    /// The email provider
    /// </summary>
    Email,

    /// <summary>
    /// The phone provider
    /// </summary>
    Phone,

    /// <summary>
    /// Google OAuth2 provider
    /// </summary>
    Google,

    /// <summary>
    /// Facebook OAuth2 provider
    /// </summary>
    Facebook,

    /// <summary>
    /// GitHub OAuth2 provider
    /// </summary>
    GitHub,

    /// <summary>
    /// Microsoft OAuth2 provider
    /// </summary>
    Microsoft,

    /// <summary>
    /// Apple OAuth2 provider
    /// </summary>
    Apple,

    /// <summary>
    /// Twitter OAuth2 provider
    /// </summary>
    Twitter,

    /// <summary>
    /// LinkedIn OAuth2 provider
    /// </summary>
    LinkedIn
} 