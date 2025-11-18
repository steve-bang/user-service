
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Steve.ManagerHero.TenantService.Domain.AggregatesModel;
// using Steve.ManagerHero.TenantService.Domain.Constants;

// namespace Steve.ManagerHero.TenantService.Infrastructure.EntityConfiguration;

// public class CustomDomainConfiguration : IEntityTypeConfiguration<CustomDomain>
// {
//     public void Configure(EntityTypeBuilder<CustomDomain> builder)
//     {
//         builder.ToTable("custom_domain");

//         builder.HasKey(us => us.Id);

//         builder.Property(rp => rp.Id)
//             .ValueGeneratedNever();

//         builder.Property(x => x.TenantId).HasColumnName("tenant_id").IsRequired();
//         builder.Property(x => x.Domain).HasColumnName("domain").IsRequired();
//         builder.Property(x => x.IsPrimary).HasColumnName("is_primary").IsRequired();
//         builder.Property(x => x.IsVerified).HasColumnName("is_verified").IsRequired();
//         builder.Property(x => x.VerifiedAt).HasColumnName("verified_at").IsRequired(false);
//         builder.Property(x => x.VerificationMethod).HasColumnName("verification_method").IsRequired();
//         builder.Property(x => x.VerificationToken).HasColumnName("verification_token").IsRequired();
//         builder.Property(x => x.VerificationRecord).HasColumnName("verification_record").IsRequired();
//         builder.Property(x => x.SslCertificateId).HasColumnName("ssl_certificate_id").IsRequired();

//         builder.Property(x => x.SslStatus).HasColumnName("ssl_status")
//             .HasConversion(
//                 v => (short)v,
//                 v => (SslStatus)v
//             ).IsRequired();

//         builder.Property(x => x.SslExpiredAt).HasColumnName("ssl_expired_at").IsRequired(false);

//         builder.Property(x => x.CreatedAt)
//             .HasColumnName("created_at")
//             .IsRequired()
//             .HasDefaultValueSql("CURRENT_TIMESTAMP");

//         builder.HasOne(x => x.Tenant)
//             .WithMany()
//             .HasForeignKey(e => e.TenantId)
//             .OnDelete(DeleteBehavior.Cascade);
//     }
// }