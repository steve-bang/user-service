// using Microsoft.EntityFrameworkCore;
// using Steve.ManagerHero.TenantService.Domain.AggregatesModel;
// using Steve.ManagerHero.TenantService.Domain.Constants;
// using Steve.ManagerHero.TenantService.Domain.Entities;

// namespace Steve.ManagerHero.TenantService.Infrastructure.EntityConfiguration;

// public class TennantPolicyConfiguration : IEntityTypeConfiguration<TenantPolicy>
// {
//     public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TenantPolicy> builder)
//     {
//         builder.ToTable("tenant_policy");

//         builder.HasKey(ta => ta.Id);

//         builder.Property(ta => ta.Id)
//             .ValueGeneratedNever();

//         builder.Property(ta => ta.TenantId).HasColumnName("tenant_id").IsRequired();

//         builder.Property(ta => ta.Type).HasColumnName("type")
//             .HasConversion(
//                 v => v.ToString(),
//                 v => Enum.Parse<TenantPolicyType>(v)
//             );

//         builder.Property(ta => ta.IsActive).HasColumnName("is_active").IsRequired();

//         builder.Property(ta => ta.Metadata).HasColumnName("metadata")
//             .HasConversion(
//                 v => System.Text.Json.JsonSerializer.Serialize(v, (System.Text.Json.JsonSerializerOptions?)null),
//                 v => System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(v, (System.Text.Json.JsonSerializerOptions?)null) ?? new Dictionary<string, string>()
//             ).IsRequired();

//         builder.Property(ta => ta.UpdatedAt)
//             .HasColumnName("updated_at")
//             .IsRequired(false);

//         builder.Property(ta => ta.CreatedAt)
//             .HasColumnName("created_at")
//             .IsRequired()
//             .HasDefaultValueSql("CURRENT_TIMESTAMP");
            

//         builder.HasOne(x => x.Tenant)
//             .WithMany()
//             .HasForeignKey(e => e.TenantId)
//             .OnDelete(DeleteBehavior.Cascade);
//     }
// }