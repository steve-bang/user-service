
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steve.ManagerHero.UserService.Domain.Constants;

namespace Steve.ManagerHero.UserService.Infrastructure.EntityConfiguration;

public class UserIdentityConfiguration : IEntityTypeConfiguration<UserIdentity>
{
    public void Configure(EntityTypeBuilder<UserIdentity> builder)
    {
        builder.ToTable("User_Identity");

        builder.HasKey(us => us.Id);

        builder.Property(us => us.ProviderId)
            .HasColumnName("provider_id")
            .IsRequired()
            .HasMaxLength(1000);

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        builder.Property(us => us.IdentityData)
            .HasColumnName("identity_data")
            .HasConversion(
                x => JsonSerializer.Serialize(x, jsonOptions),
                x => JsonSerializer.Deserialize<IDictionary<string, object>>(x, jsonOptions)
                    ?? new Dictionary<string, object>()
            ).IsRequired(false);

        builder.Property(u => u.Provider)
            .HasColumnName("provider")
            .HasConversion(
                x => x.ToString(),
                x => Enum.Parse<IdentityProvider>(x)
            ).IsRequired(true);


        builder.Property(us => us.UserId)
            .HasColumnName("user_id");

        builder.HasOne(us => us.User)
            .WithMany(u => u.Identities)
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(us => us.UserId);

        builder.Property(u => u.LastLoginAt)
            .HasColumnName("last_login_at");

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired(false);

        builder.Property(us => us.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}