/*
* Author: Steve Bang
* History:
* - [2024-04-11] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steve.ManagerHero.UserService.Domain.AggregatesModel;

namespace Steve.ManagerHero.UserService.Infrastructure.EntityConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("name");

        builder.Property(r => r.Description)
            .HasMaxLength(500)
            .HasColumnName("description");

        builder.Property(r => r.IsDefault)
            .HasDefaultValue(false)
            .HasColumnName("is_default");

        // Timestamps
        builder.Property(r => r.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder
            .HasMany(r => r.UserRoles)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId);

        builder.HasIndex(r => r.Name)
            .IsUnique();
    }
}