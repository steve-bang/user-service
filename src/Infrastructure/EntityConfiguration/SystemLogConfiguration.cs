
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Steve.ManagerHero.UserService.Domain.Entities;

namespace Steve.ManagerHero.UserService.Infrastructure.EntityConfiguration;

public class SystemLogConfiguration : IEntityTypeConfiguration<SystemLogEntity>
{
    public void Configure(EntityTypeBuilder<SystemLogEntity> builder)
    {
        builder.ToTable("System_Log");

        builder.HasKey(us => us.Id);

        builder.Property(rp => rp.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired(false);
        builder.Property(x => x.CorrelationId).HasColumnName("correlation_id").HasMaxLength(200);

        builder.Property(x => x.HttpMethod).HasColumnName("http_method").HasMaxLength(10).IsRequired();
        builder.Property(x => x.Path).HasColumnName("path").HasMaxLength(2000).IsRequired();
        builder.Property(x => x.StatusCode).HasColumnName("status_code").IsRequired();
        builder.Property(x => x.DurationMs).HasColumnName("duration_ms");

        builder.Property(x => x.IpAddress).HasColumnName("ip_address").HasMaxLength(100);
        builder.Property(x => x.UserAgent).HasColumnName("user_agent").HasMaxLength(2000);

        builder.Property(x => x.RequestHeaders).HasColumnName("request_headers").HasColumnType("text");
        builder.Property(x => x.ResponseHeaders).HasColumnName("response_headers").HasColumnType("text");
        builder.Property(x => x.RequestBodySnippet).HasColumnName("request_body_snippet").HasColumnType("text");
        builder.Property(x => x.ResponseBodySnippet).HasColumnName("response_body_snippet").HasColumnType("text");



        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

    }
}