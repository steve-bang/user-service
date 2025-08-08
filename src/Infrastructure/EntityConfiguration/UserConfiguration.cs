/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steve.ManagerHero.UserService.Domain.ValueObjects;

namespace Steve.ManagerHero.UserService.Infrastructure.EntityConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Table name
        builder.ToTable("User"); // PostgreSQL convention: lowercase

        // Primary Key
        builder.HasKey(u => u.Id)
            .HasName("pk_users");

        // Property configurations
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("first_name");

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("last_name");

        builder.Property(u => u.DisplayName)
            .IsRequired()
            .HasColumnName("display_name")
            .HasComputedColumnSql("first_name || ' ' || last_name", stored: true);

        // Value Objects stored as columns
        builder.Property(u => u.EmailAddress)
            .HasConversion(
                v => v.Value,
                v => new EmailAddress(v))
            .IsRequired()
            .HasMaxLength(254) // RFC 5321 max length
            .HasColumnName("email_address");

        builder.Property(u => u.SecondaryEmailAddress)
            .HasConversion(
                v => v!.Value,
                v => v != null ? new EmailAddress(v) : null)
            .HasMaxLength(254)
            .HasColumnName("secondary_email")
            .IsRequired(false);

        builder.Property(u => u.PhoneNumber)
            .HasConversion(
                v => v!.Value,
                v => v != null ? new PhoneNumber(v) : null)
            .HasMaxLength(20)
            .HasColumnName("phone_number")
            .IsRequired(false);


        // Address as JSONB (PostgreSQL specific)
        builder.OwnsOne(u => u.Address, a =>
        {
            a.ToJson(); // Stores as JSONB in PostgreSQL
            a.Property(x => x.Street).HasColumnName("street");
            a.Property(x => x.City).HasColumnName("city");
            a.Property(x => x.State).HasColumnName("state");
            a.Property(x => x.ZipCode).HasColumnName("zip_code");
            a.Property(x => x.CountryCode).HasColumnName("country_code");
        });

        // Password Hash as separate columns

        // When the user auth with OAuth method, the password is null
        builder.OwnsOne(u => u.PasswordHash, passwordHash =>
            {
                // Configure the Hash property
                passwordHash.Property(ph => ph.Hash)
                    .HasColumnName("password_hash")
                    .HasColumnType("text")
                    .IsRequired(false);

                // Configure the Salt property
                passwordHash.Property(ph => ph.Salt)
                    .HasColumnName("password_salt")
                    .HasColumnType("text")
                    .IsRequired(false);
            });

        // Status properties
        builder.Property(u => u.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20)
            .HasColumnName("status");

        builder.Property(u => u.IsActive)
            .IsRequired()
            .HasDefaultValue(true)
            .HasColumnName("is_active");

        builder.Property(u => u.EmailVerifiedAt)
            .HasColumnName("email_verified_at")
            .IsRequired(false);

        builder.Property(u => u.IsEmailVerified)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName("is_email_verified");

        builder.Property(u => u.PhoneVerifiedAt)
            .HasColumnName("phone_verified_at")
            .IsRequired(false);

        builder.Property(u => u.IsPhoneVerified)
            .IsRequired()
            .HasDefaultValue(false)
            .HasColumnName("is_phone_verified");

        // Timestamps
        builder.Property(u => u.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired(false);

        builder.Property(u => u.PasswordChangedDate)
            .HasColumnName("password_changed_date")
            .IsRequired(false);

        builder.Property(u => u.LastLoginDate)
            .HasColumnName("last_login_date")
            .IsRequired(false);

        // Relationships
        // builder
        //     .HasMany<UserRole>("_userRoles")
        //     .WithOne(ur => ur.User)
        //     .HasForeignKey(ur => ur.UserId)
        //     .OnDelete(DeleteBehavior.Cascade)
        //     .IsRequired(false);


        // Indexes
        builder.HasIndex(u => u.EmailAddress)
            .IsUnique()
            .HasDatabaseName("ix_users_email");

        builder.HasIndex(u => u.DisplayName)
            .HasDatabaseName("ix_users_display_name");

        builder.HasIndex(u => u.IsActive)
            .HasDatabaseName("ix_users_is_active");

        builder.HasIndex(u => u.Status)
            .HasDatabaseName("ix_users_status");

        // Configure the navigation with field access, this is required because the navigation is a collection of value objects
        // Configure private field _userRoles as backing field for UserRoles
        // builder.Metadata
        //     .FindNavigation(nameof(User.UserRoles))!
        //     .SetPropertyAccessMode(PropertyAccessMode.Field);

        // Soft delete filter
        //builder.HasQueryFilter(u => u.IsActive);
    }
}