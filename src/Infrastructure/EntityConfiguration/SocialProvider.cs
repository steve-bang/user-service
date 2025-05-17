/*
* Author: Steve Bang
* History:
* - [2025-05-17] - Created by mrsteve.bang@gmail.com
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Steve.ManagerHero.UserService.Infrastructure.EntityConfiguration;

public class SocialProviderConfiguration : IEntityTypeConfiguration<SocialProvider>
{
    public void Configure(EntityTypeBuilder<SocialProvider> builder)
    {
        builder.ToTable("Social_Provider");

        builder.HasKey(sp => sp.Id);

        builder.Property(sp => sp.Name)
            .HasColumnName("name")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(sp => sp.DisplayName)
            .HasColumnName("display_name")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(sp => sp.Type)
            .HasColumnName("type")
            .HasColumnType("integer")
            .HasConversion<int>()
            .IsRequired();

        // OAuth2Credentials value object
        builder.OwnsOne(sp => sp.Credentials, credentials =>
        {
            credentials.Property(c => c.ClientId)
                .HasColumnName("client_id")
                .HasColumnType("text")
                .IsRequired();

            credentials.Property(c => c.ClientSecret)
                .HasColumnName("client_secret")
                .HasColumnType("text")
                .IsRequired();
        });

        // OAuth2Endpoints value object
        builder.OwnsOne(sp => sp.Endpoints, endpoints =>
        {
            endpoints.Property(e => e.AuthorizationEndpoint)
                .HasColumnName("authorization_endpoint")
                .HasColumnType("text")
                .IsRequired();

            endpoints.Property(e => e.TokenEndpoint)
                .HasColumnName("token_endpoint")
                .HasColumnType("text")
                .IsRequired();

            endpoints.Property(e => e.UserInfoEndpoint)
                .HasColumnName("user_info_endpoint")
                .HasColumnType("text")
                .IsRequired();

            endpoints.Property(e => e.JwksUri)
                .HasColumnName("jwks_uri")
                .HasColumnType("text")
                .IsRequired();

            endpoints.Property(e => e.AdditionalParameters)
                .HasColumnName("additional_parameters")
                .HasColumnType("text");
        });

        builder.Property(sp => sp.Scopes)
            .HasColumnName("scopes")
            .HasColumnType("text[]")
            .IsRequired();

        builder.Property(sp => sp.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(sp => sp.AutoLinkAccounts)
            .HasColumnName("auto_link_accounts")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(sp => sp.AllowRegistration)
            .HasColumnName("allow_registration")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(sp => sp.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(sp => sp.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Configure navigation property
        builder.HasMany(sp => sp.SocialUsers)
            .WithOne()
            .HasForeignKey("provider_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}