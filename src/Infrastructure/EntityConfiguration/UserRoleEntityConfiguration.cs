/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Steve.ManagerHero.UserService.Infrastructure.EntityConfiguration;

public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("user_role");

        // Primary Key
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever();

        builder.Property(u => u.UserId)
            .HasColumnName("user_id");

        builder.Property(u => u.RoleId)
            .HasColumnName("role_id");

        builder.Property(u => u.AssignedAt)
            .HasColumnName("assigned_at")
            .IsRequired();

        builder.Property(u => u.AssignedBy)
            .HasColumnName("assigned_by")
            .IsRequired(false);

        builder.HasKey(u => new { u.UserId, u.RoleId }); // Composite Key

        builder
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        builder
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
    }
}