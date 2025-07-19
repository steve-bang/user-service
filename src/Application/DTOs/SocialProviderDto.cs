/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Application.DTOs;

public class SocialProviderDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public IdentityProviderType Type { get; set; }
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string[] Scopes { get; set; } = null!;
    public string AuthorizationEndpoint { get; set; } = null!;
    public string TokenEndpoint { get; set; } = null!;
    public string UserInfoEndpoint { get; set; } = null!;
    public string JwksUri { get; set; } = null!;
    public string? AdditionalParameters { get; set; }
    public bool IsActive { get; set; }
    public bool AutoLinkAccounts { get; set; }
    public bool AllowRegistration { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateSocialProviderDto
{
    public string Name { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public IdentityProviderType Type { get; set; }
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string[] Scopes { get; set; } = null!;
    public string AuthorizationEndpoint { get; set; } = null!;
    public string TokenEndpoint { get; set; } = null!;
    public string UserInfoEndpoint { get; set; } = null!;
    public string JwksUri { get; set; } = null!;
    public string? AdditionalParameters { get; set; }
    public bool IsActive { get; set; }
    public bool AutoLinkAccounts { get; set; }
    public bool AllowRegistration { get; set; }
}

public class UpdateSocialProviderDto
{
    public string DisplayName { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string[] Scopes { get; set; } = null!;
    public string AuthorizationEndpoint { get; set; } = null!;
    public string TokenEndpoint { get; set; } = null!;
    public string UserInfoEndpoint { get; set; } = null!;
    public string JwksUri { get; set; } = null!;
    public string? AdditionalParameters { get; set; }
    public bool IsActive { get; set; }
    public bool AutoLinkAccounts { get; set; }
    public bool AllowRegistration { get; set; }
} 