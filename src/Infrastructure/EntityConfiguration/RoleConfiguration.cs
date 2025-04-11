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
            .HasMaxLength(100);
            
        builder.Property(r => r.Description)
            .HasMaxLength(500);
            
        builder.Property(r => r.IsDefault)
            .HasDefaultValue(false);
            
        builder.Property(r => r.CreatedAt)
            .IsRequired();
            
        builder.HasIndex(r => r.Name)
            .IsUnique();
    }
}