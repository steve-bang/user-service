/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Constants;
using Steve.ManagerHero.UserService.Domain.Events;
using Steve.ManagerHero.UserService.Domain.ValueObjects;

namespace Steve.ManagerHero.UserService.Domain.AggregatesModel;

/// <summary>
/// Represents a social provider configuration for OAuth2 authentication
/// </summary>
public class SocialProvider : AggregateRoot
{
    public string Name { get; private set; }
    public string DisplayName { get; private set; }
    public SocialProviderType Type { get; private set; }
    public OAuth2Credentials Credentials { get; private set; }
    public OAuth2Endpoints Endpoints { get; private set; }
    public string[] Scopes { get; private set; }
    public bool IsActive { get; private set; }
    public bool AutoLinkAccounts { get; private set; }
    public bool AllowRegistration { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Navigation properties
    private readonly List<SocialUser> _socialUsers = new();
    public IReadOnlyCollection<SocialUser> SocialUsers => _socialUsers.AsReadOnly();

    /// <summary>
    /// Initialize a new social provider
    /// </summary>
    private SocialProvider() { }

    /// <summary>
    /// Initialize a new social provider
    /// </summary>
    private SocialProvider(
        string name,
        string displayName,
        SocialProviderType type,
        OAuth2Credentials credentials,
        OAuth2Endpoints endpoints,
        string[] scopes,
        bool isActive,
        bool autoLinkAccounts,
        bool allowRegistration
    )
    {
        ValidateName(name);
        ValidateDisplayName(displayName);
        ValidateScopes(scopes);

        Name = name;
        DisplayName = displayName;
        Type = type;
        Credentials = credentials;
        Endpoints = endpoints;
        Scopes = scopes;
        IsActive = isActive;
        AutoLinkAccounts = autoLinkAccounts;
        AllowRegistration = allowRegistration;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;

        AddEvent(new SocialProviderCreatedEvent(this));
    }

    /// <summary>
    /// Create a new social provider
    /// </summary>
    public static SocialProvider Create(
        string name,
        string displayName,
        SocialProviderType type,
        string clientId,
        string clientSecret,
        string[] scopes,
        string authorizationEndpoint,
        string tokenEndpoint,
        string userInfoEndpoint,
        string jwksUri,
        string additionalParameters,
        bool isActive,
        bool autoLinkAccounts,
        bool allowRegistration
    )
    {
        var credentials = new OAuth2Credentials(clientId, clientSecret);
        var endpoints = new OAuth2Endpoints(
            authorizationEndpoint,
            tokenEndpoint,
            userInfoEndpoint,
            jwksUri,
            additionalParameters
        );

        return new SocialProvider(
            name,
            displayName,
            type,
            credentials,
            endpoints,
            scopes,
            isActive,
            autoLinkAccounts,
            allowRegistration
        );
    }

    /// <summary>
    /// Update a social provider
    /// </summary>
    public void Update(
        string displayName,
        string clientId,
        string clientSecret,
        string[] scopes,
        string authorizationEndpoint,
        string tokenEndpoint,
        string userInfoEndpoint,
        string jwksUri,
        string additionalParameters,
        bool isActive,
        bool autoLinkAccounts,
        bool allowRegistration
    )
    {
        ValidateDisplayName(displayName);
        ValidateScopes(scopes);

        DisplayName = displayName;
        Credentials = new OAuth2Credentials(clientId, clientSecret);
        Endpoints = new OAuth2Endpoints(
            authorizationEndpoint,
            tokenEndpoint,
            userInfoEndpoint,
            jwksUri,
            additionalParameters
        );
        Scopes = scopes;
        IsActive = isActive;
        AutoLinkAccounts = autoLinkAccounts;
        AllowRegistration = allowRegistration;
        UpdatedAt = DateTime.UtcNow;

        AddEvent(new SocialProviderUpdatedEvent(this,
        DisplayName,
        IsActive,
        AutoLinkAccounts,
        AllowRegistration,
        Scopes));
    }

    /// <summary>
    /// Activate the social provider
    /// </summary>
    public void Activate()
    {
        if (IsActive) return;

        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
        AddEvent(new SocialProviderActivatedEvent(this));
    }

    /// <summary>
    /// Deactivate the social provider
    /// </summary>
    public void Deactivate()
    {
        if (!IsActive) return;

        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
        AddEvent(new SocialProviderDeactivatedEvent(this));
    }

    /// <summary>
    /// Get the authorization URL with the given redirect URI and state
    /// </summary>
    public string GetAuthorizationUrl(string redirectUri, string state)
    {
        if (!IsActive)
            throw new InvalidOperationException(Name);

        var parameters = new Dictionary<string, string>
        {
            ["client_id"] = Credentials.ClientId,
            ["redirect_uri"] = redirectUri,
            ["response_type"] = "code",
            ["scope"] = string.Join(" ", Scopes),
            ["state"] = state
        };

        if (!string.IsNullOrEmpty(Endpoints.AdditionalParameters))
        {
            var additionalParams = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(
                Endpoints.AdditionalParameters
            );
            foreach (var param in additionalParams)
            {
                parameters[param.Key] = param.Value;
            }
        }

        var queryString = string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));
        return $"{Endpoints.AuthorizationEndpoint}?{queryString}";
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidOperationException("Name cannot be empty");

        if (name.Length > 50)
            throw new InvalidOperationException("Name cannot be longer than 50 characters");
    }

    private static void ValidateDisplayName(string displayName)
    {
        if (string.IsNullOrWhiteSpace(displayName))
            throw new InvalidOperationException("Display name cannot be empty");

        if (displayName.Length > 100)
            throw new InvalidOperationException("Display name cannot be longer than 100 characters");
    }

    private static void ValidateScopes(string[] scopes)
    {
        if (scopes == null || scopes.Length == 0)
            throw new InvalidOperationException("At least one scope is required");

        if (scopes.Any(string.IsNullOrWhiteSpace))
            throw new InvalidOperationException("Scopes cannot be empty");
    }
}