
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Infrastructure.EntityConfiguration;

public class OtpConfiguration : IEntityTypeConfiguration<Otp>
{
    public void Configure(EntityTypeBuilder<Otp> builder)
    {
        builder.ToTable("otp");

        builder.HasKey(us => us.Id);

        builder.Property(rp => rp.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired(false);
        builder.Property(x => x.PhoneNumber).HasColumnName("phone_number").IsRequired();
        builder.Property(x => x.OtpHash).HasColumnName("otp_hash").HasColumnType("text").IsRequired();
        builder.Property(x => x.Salt).HasColumnName("salt").HasColumnType("text").IsRequired();
        builder.Property(x => x.ExpirationTime).HasColumnName("expiration_time").IsRequired();
        builder.Property(x => x.ConsumedAt).HasColumnName("consumed_at").IsRequired(false);
        builder.Property(x => x.IsUsed).HasColumnName("is_used").HasDefaultValue(false).IsRequired();
        builder.Property(x => x.RetryCount).HasColumnName("retry_count").HasDefaultValue(0).IsRequired();
        builder.Property(x => x.Type)
        .HasColumnName("type")
        .HasConversion(
            v => (short)v,
            v => (OtpType)v
        );

        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(us => us.User)
            .WithMany(u => u.Otps)
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}