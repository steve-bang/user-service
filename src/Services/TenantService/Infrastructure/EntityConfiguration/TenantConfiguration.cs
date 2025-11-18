
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Newtonsoft.Json;
// using Steve.ManagerHero.TenantService.Domain.AggregatesModel;
// using Steve.ManagerHero.TenantService.Domain.Constants;

// namespace Steve.ManagerHero.TenantService.Infrastructure.EntityConfiguration;

// public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
// {
//     public void Configure(EntityTypeBuilder<Tenant> builder)
//     {
//         builder.ToTable("tenant");

//         builder.HasKey(us => us.Id);

//         builder.Property(rp => rp.Id)
//             .ValueGeneratedNever();

//         builder.Property(x => x.Name).HasColumnName("name").IsRequired();
//         builder.Property(x => x.Domain).HasColumnName("domain").IsRequired();
//         builder.Property(x => x.Description).HasColumnName("description").IsRequired();
//         builder.Property(x => x.TrialsEndAt).HasColumnName("trials_end_at").IsRequired(false);
//         builder.Property(x => x.SubscriptionEndAt).HasColumnName("subscription_end_at").IsRequired(false);

//         builder.Property(x => x.Status).HasColumnName("status")
//             .HasConversion(
//                 v => (short)v,
//                 v => (TenantStatus)v
//             ).IsRequired();

//         builder.Property(x => x.Branding).HasColumnName("branding")
//             .HasConversion(
//                 v => JsonConvert.SerializeObject(v),
//                 v => JsonConvert.DeserializeObject<IDictionary<string, string>?>(v)
//             ).IsRequired(false);

//         builder.Property(x => x.Metadata).HasColumnName("metadata")
//             .HasConversion(
//                 v => JsonConvert.SerializeObject(v),
//                 v => JsonConvert.DeserializeObject<IDictionary<string, string>?>(v)
//             ).IsRequired(false);

//         builder.Property(x => x.UpdatedAt)
//             .HasColumnName("updated_at")
//             .IsRequired(false);

//         builder.Property(x => x.CreatedAt)
//             .HasColumnName("created_at")
//             .IsRequired()
//             .HasDefaultValueSql("CURRENT_TIMESTAMP");

//         // Indexes
//         builder.HasIndex(u => u.Domain)
//             .HasDatabaseName("ix_domain");

//     }
// }