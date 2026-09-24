using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using VelascoPersonalWebsite_IPT.DataAccess.Entities;
using VelascoPersonalWebsite_IPT.DataAccess.Enums;

namespace VelascoPersonalWebsite_IPT.DataAccess.Configurations
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(user => user.UserId);

            builder.Property(user => user.Username)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(user => user.Username)
                .IsUnique();

            builder.Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(254);

            builder.HasIndex(user => user.Email)
                .IsUnique();

            builder.Property(user => user.PasswordHash)
                .IsRequired()
                .HasMaxLength(512);

            builder.Property(user => user.DateOfBirth)
                .IsRequired();

            builder.Property(user => user.EmploymentStatus)
                .IsRequired();

            builder.Property(user => user.Gender)
                .IsRequired();

            builder.Property(user => user.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(user => user.IsOnline)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(user => user.Role)
                .IsRequired()
                .HasDefaultValue(Role.User);

            builder.Property(user => user.RegisteredUtc)
                .IsRequired();
        }
    }
}
