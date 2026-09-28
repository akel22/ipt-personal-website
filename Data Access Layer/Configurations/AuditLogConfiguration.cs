using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using VelascoPersonalWebsite_IPT.DataAccess.Entities;

namespace VelascoPersonalWebsite_IPT.DataAccess.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.ToTable("AuditLogs");
            builder.HasKey(log => log.AuditLogId);

            builder.Property(log => log.EventType)
                .IsRequired();

            builder.Property(log => log.OccurredUtc)
                .IsRequired();

            builder.HasOne(log => log.User)
                .WithMany()
                .HasForeignKey(log => log.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(log => new { log.UserId, log.OccurredUtc });
            builder.HasIndex(log => new { log.EventType, log.OccurredUtc });
        }
    }
}
