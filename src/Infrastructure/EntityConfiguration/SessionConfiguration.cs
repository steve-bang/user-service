
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Steve.ManagerHero.UserService.Infrastructure.EntityConfiguration;

public class UserSessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("Session");

        builder.HasKey(us => us.Id);

        builder.Property(us => us.AccessToken)
            .HasColumnName("access_token")
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(us => us.RefreshToken)
            .HasColumnName("refresh_token")
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(us => us.IpAddress)
            .HasColumnName("ip_address")
            .HasMaxLength(45); // Supports IPv6

        builder.Property(us => us.UserAgent)
            .HasColumnName("user_agent")
            .HasMaxLength(500);


        builder.Property(us => us.ExpiresAt)
            .HasColumnName("expires_at")
            .IsRequired();

        builder.Property(us => us.IsActive)
            .HasColumnName("is_active")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(us => us.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(us => us.UserId)
            .HasColumnName("user_id");

        builder.HasOne(us => us.User)
            .WithMany(u => u.Sessions)
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(us => us.AccessToken)
            .IsUnique();

        builder.HasIndex(us => us.RefreshToken)
            .IsUnique();

        builder.HasIndex(us => us.UserId);
    }
}