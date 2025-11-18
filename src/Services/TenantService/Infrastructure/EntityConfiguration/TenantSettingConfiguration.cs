// using Microsoft.EntityFrameworkCore;
// using Steve.ManagerHero.TenantService.Domain.Entities;

// namespace Steve.ManagerHero.TenantService.Infrastructure.EntityConfiguration;

// public class TenantSettingConfiguration : IEntityTypeConfiguration<TenantSettingEntity>
// {
//     public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TenantSettingEntity> builder)
//     {
//         builder.ToTable("tenant_setting");

//         builder.HasKey(ts => ts.Id);

//         builder.Property(ts => ts.Id)
//             .ValueGeneratedNever();

//         builder.Property(ts => ts.TenantId).HasColumnName("tenant_id");

//         builder.Property(ts => ts.FriendlyName)
//             .HasColumnName("friendly_name")
//             .IsRequired(false);

//         builder.Property(ts => ts.LogoUrl)
//             .HasColumnName("logo_url")
//             .IsRequired(false);

//         builder.Property(ts => ts.SupportEmail)
//             .HasColumnName("support_email")
//             .IsRequired(false);

//         builder.Property(ts => ts.SupportUrl)
//             .HasColumnName("support_url")
//             .IsRequired(false);

//         builder.HasOne(x => x.Tenant)
//                .WithOne(t => t.Setting) 
//                .HasForeignKey<TenantSettingEntity>(x => x.TenantId);
//     }
// }