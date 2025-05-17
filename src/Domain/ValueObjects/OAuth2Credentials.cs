/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

namespace Steve.ManagerHero.UserService.Domain.ValueObjects;

/// <summary>
/// Represents OAuth2 credentials for a social provider
/// </summary>
public class OAuth2Credentials
{
    public string ClientId { get; }
    public string ClientSecret { get; }

    private OAuth2Credentials() { }

    public OAuth2Credentials(string clientId, string clientSecret)
    {
        if (string.IsNullOrWhiteSpace(clientId))
            throw new ArgumentException("Client ID cannot be empty", nameof(clientId));

        if (string.IsNullOrWhiteSpace(clientSecret))
            throw new ArgumentException("Client Secret cannot be empty", nameof(clientSecret));

        ClientId = clientId;
        ClientSecret = clientSecret;
    }
} 