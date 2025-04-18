/*
* Author: Steve Bang
* History:
* - [2025-04-11] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steve.ManagerHero.UserService.Domain.AggregatesModel;

namespace Steve.ManagerHero.UserService.Infrastructure.EntityConfiguration;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("Role_Permission");
        
        builder.HasKey(rp => rp.Id);
        
        builder.HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);
            
        builder.HasOne(rp => rp.Permission)
            .WithMany(p => p.RolePermissions)
            .HasForeignKey(rp => rp.PermissionId);
            
        builder.HasIndex(rp => new { rp.RoleId, rp.PermissionId })
            .IsUnique();
    }
}