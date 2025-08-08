/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.ValueObjects;

/// <summary>
/// Represents OAuth2 endpoints for a social provider
/// </summary>
public class OAuth2Endpoints
{
    public string AuthorizationEndpoint { get; }
    public string TokenEndpoint { get; }
    public string UserInfoEndpoint { get; }
    public string JwksUri { get; }
    public string AdditionalParameters { get; }

    private OAuth2Endpoints() { }

    public OAuth2Endpoints(
        string authorizationEndpoint,
        string tokenEndpoint,
        string userInfoEndpoint,
        string jwksUri,
        string additionalParameters
    )
    {
        if (string.IsNullOrWhiteSpace(authorizationEndpoint))
            throw new ArgumentException("Authorization endpoint cannot be empty", nameof(authorizationEndpoint));

        if (string.IsNullOrWhiteSpace(tokenEndpoint))
            throw new ArgumentException("Token endpoint cannot be empty", nameof(tokenEndpoint));

        if (string.IsNullOrWhiteSpace(userInfoEndpoint))
            throw new ArgumentException("User info endpoint cannot be empty", nameof(userInfoEndpoint));

        AuthorizationEndpoint = authorizationEndpoint;
        TokenEndpoint = tokenEndpoint;
        UserInfoEndpoint = userInfoEndpoint;
        JwksUri = jwksUri;
        AdditionalParameters = additionalParameters;
    }
} 