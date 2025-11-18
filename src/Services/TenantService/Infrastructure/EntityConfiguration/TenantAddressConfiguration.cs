// using Microsoft.EntityFrameworkCore;
// using Steve.ManagerHero.TenantService.Domain.Entities;

// namespace Steve.ManagerHero.TenantService.Infrastructure.EntityConfiguration;

// public class TennantAddressConfiguration : IEntityTypeConfiguration<TenantAddressEntity>
// {
//     public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TenantAddressEntity> builder)
//     {
//         builder.ToTable("tenant_address");

//         builder.HasKey(ta => ta.Id);

//         builder.Property(ta => ta.Id)
//             .ValueGeneratedNever();

//         builder.Property(ta => ta.TenantId).HasColumnName("tenant_id").IsRequired();
//         builder.Property(ta => ta.Line1).HasColumnName("line_1").IsRequired();
//         builder.Property(ta => ta.Line2).HasColumnName("line_2");
//         builder.Property(ta => ta.City).HasColumnName("city").IsRequired();
//         builder.Property(ta => ta.State).HasColumnName("state").IsRequired();
//         builder.Property(ta => ta.PostalCode).HasColumnName("postal_code").IsRequired();
//         builder.Property(ta => ta.Country).HasColumnName("country").IsRequired();

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