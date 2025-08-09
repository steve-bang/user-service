
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steve.ManagerHero.UserService.Domain.Entities;

namespace Steve.ManagerHero.UserService.Infrastructure.EntityConfiguration;

public class PasswordHistoryConfiguration : IEntityTypeConfiguration<PasswordHistoryEntity>
{
    public void Configure(EntityTypeBuilder<PasswordHistoryEntity> builder)
    {
        builder.ToTable("Password_History");

        builder.HasKey(us => us.Id);

        builder.Property(rp => rp.Id)
            .ValueGeneratedNever();

        // Password Hash as separate columns
        builder.Property(ph => ph.PasswordHash)
            .HasColumnName("password_hash")
            .HasColumnType("text")
            .IsRequired();

        // Configure the Salt property
        builder.Property(ph => ph.PasswordHash)
            .HasColumnName("password_salt")
            .HasColumnType("text")
            .IsRequired();


        builder.Property(us => us.UserId)
            .HasColumnName("user_id");

        builder.HasOne(us => us.User)
            .WithMany(u => u.PasswordHistories)
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(u => u.ChangedAt)
            .HasColumnName("changed_at")
            .IsRequired();

    }
}