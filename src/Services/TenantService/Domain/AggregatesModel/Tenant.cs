
// using Steve.ManagerHero.TenantService.Domain.Constants;
// using Steve.ManagerHero.TenantService.Domain.Entities;

// namespace Steve.ManagerHero.TenantService.Domain.AggregatesModel;

// public class Tenant : AggregateRoot
// {
//     public string Name { get; private set; } = default!;

//     public string Domain { get; private set; } = default!;

//     public string? Description { get; private set; }

//     public TenantStatus Status { get; private set; }

//     public IDictionary<string, string>? Branding { get; private set; }

//     public IDictionary<string, string>? Metadata { get; private set; }

//     public DateTime? TrialsEndAt { get; private set; }

//     public DateTime? SubscriptionEndAt { get; private set; }

//     public TenantSettingEntity Setting { get; private set; } = default!;

//     public DateTime? UpdatedAt { get; private set; }

//     public DateTime CreatedAt { get; private set; }

//     public Tenant(string name, string domain, string? description = null, IDictionary<string, string>? branding = null, IDictionary<string, string>? metadata = null, DateTime? trialsEndAt = null, DateTime? subscriptionEndAt = null)
//     {
//         if (string.IsNullOrWhiteSpace(name))
//             throw new ArgumentException("Name is required.", nameof(name));

//         if (string.IsNullOrWhiteSpace(domain))
//             throw new ArgumentException("Domain is required.", nameof(domain));

//         Name = name;
//         Domain = domain.ToLower();
//         Description = description;
//         Status = TenantStatus.Active;
//         Branding = branding;
//         Metadata = metadata;
//         TrialsEndAt = trialsEndAt;
//         SubscriptionEndAt = subscriptionEndAt;
//         Setting = new TenantSettingEntity();
//         CreatedAt = DateTime.UtcNow;
//     }

//     public void Update(string name, string? description = null, IDictionary<string, string>? branding = null, IDictionary<string, string>? metadata = null)
//     {
//         Name = name;
//         Description = description;
//         Branding = branding;
//         Metadata = metadata;
//         UpdatedAt = DateTime.UtcNow;
//     }
// }