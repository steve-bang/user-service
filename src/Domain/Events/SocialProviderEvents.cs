/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Common;
using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Domain.Events;

/// <summary>
/// Base class for social provider events
/// </summary>
public abstract class SocialProviderEvent : IDomainEvent
{
    public Guid ProviderId { get; }
    public string ProviderName { get; }
    public SocialProviderType ProviderType { get; }
    public DateTime OccurredAt { get; }
    public string EventType { get; }

    protected SocialProviderEvent(SocialProvider provider)
    {
        ProviderId = provider.Id;
        ProviderName = provider.Name;
        ProviderType = provider.Type;
        OccurredAt = DateTime.UtcNow;
        EventType = GetType().Name;
    }
}

/// <summary>
/// Event raised when a social provider is created
/// </summary>
public class SocialProviderCreatedEvent : SocialProviderEvent
{
    public string DisplayName { get; }
    public bool IsActive { get; }
    public bool AutoLinkAccounts { get; }
    public bool AllowRegistration { get; }
    public string[] Scopes { get; }

    public SocialProviderCreatedEvent(SocialProvider provider) : base(provider)
    {
        DisplayName = provider.DisplayName;
        IsActive = provider.IsActive;
        AutoLinkAccounts = provider.AutoLinkAccounts;
        AllowRegistration = provider.AllowRegistration;
        Scopes = provider.Scopes;
    }
}

/// <summary>
/// Event raised when a social provider is updated
/// </summary>
public class SocialProviderUpdatedEvent : SocialProviderEvent
{
    public string OldDisplayName { get; }
    public string NewDisplayName { get; }
    public bool OldIsActive { get; }
    public bool NewIsActive { get; }
    public bool OldAutoLinkAccounts { get; }
    public bool NewAutoLinkAccounts { get; }
    public bool OldAllowRegistration { get; }
    public bool NewAllowRegistration { get; }
    public string[] OldScopes { get; }
    public string[] NewScopes { get; }

    public SocialProviderUpdatedEvent(
        SocialProvider provider,
        string oldDisplayName,
        bool oldIsActive,
        bool oldAutoLinkAccounts,
        bool oldAllowRegistration,
        string[] oldScopes
    ) : base(provider)
    {
        OldDisplayName = oldDisplayName;
        NewDisplayName = provider.DisplayName;
        OldIsActive = oldIsActive;
        NewIsActive = provider.IsActive;
        OldAutoLinkAccounts = oldAutoLinkAccounts;
        NewAutoLinkAccounts = provider.AutoLinkAccounts;
        OldAllowRegistration = oldAllowRegistration;
        NewAllowRegistration = provider.AllowRegistration;
        OldScopes = oldScopes;
        NewScopes = provider.Scopes;
    }
}

/// <summary>
/// Event raised when a social provider is activated
/// </summary>
public class SocialProviderActivatedEvent : SocialProviderEvent
{
    public DateTime ActivatedAt { get; }

    public SocialProviderActivatedEvent(SocialProvider provider) : base(provider)
    {
        ActivatedAt = DateTime.UtcNow;
    }
}

/// <summary>
/// Event raised when a social provider is deactivated
/// </summary>
public class SocialProviderDeactivatedEvent : SocialProviderEvent
{
    public DateTime DeactivatedAt { get; }

    public SocialProviderDeactivatedEvent(SocialProvider provider) : base(provider)
    {
        DeactivatedAt = DateTime.UtcNow;
    }
}

/// <summary>
/// Event raised when a social provider's OAuth2 credentials are updated
/// </summary>
public class SocialProviderCredentialsUpdatedEvent : SocialProviderEvent
{
    public DateTime UpdatedAt { get; }

    public SocialProviderCredentialsUpdatedEvent(SocialProvider provider) : base(provider)
    {
        UpdatedAt = DateTime.UtcNow;
    }
}

/// <summary>
/// Event raised when a social provider's OAuth2 endpoints are updated
/// </summary>
public class SocialProviderEndpointsUpdatedEvent : SocialProviderEvent
{
    public DateTime UpdatedAt { get; }

    public SocialProviderEndpointsUpdatedEvent(SocialProvider provider) : base(provider)
    {
        UpdatedAt = DateTime.UtcNow;
    }
} 