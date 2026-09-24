using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using VelascoPersonalWebsite_IPT.DataAccess.Entities;

namespace VelascoPersonalWebsite_IPT.DataAccess.Configurations
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("Profiles");
            builder.HasKey(profile => profile.ProfileId);

            builder.Property(profile => profile.ProfileId)
                .ValueGeneratedOnAdd();

            builder.Property(profile => profile.UpdatedUtc)
                .IsRequired();

            builder.HasIndex(profile => profile.UpdatedUtc);

            builder.HasCheckConstraint("CK_Profiles_Singleton", "[ProfileId] = 1");

            foreach (var property in new[]
            {
                nameof(Profile.GreetingName), nameof(Profile.Introduction), nameof(Profile.Email),
                nameof(Profile.LinkedInUrl), nameof(Profile.GitHubUrl), nameof(Profile.EducationHeading),
                nameof(Profile.EducationSummary), nameof(Profile.CurrentEducationPeriod), nameof(Profile.CurrentEducationTitle),
                nameof(Profile.CurrentEducationDescription), nameof(Profile.PreviousEducationPeriod), nameof(Profile.PreviousEducationTitle),
                nameof(Profile.PreviousEducationDescription), nameof(Profile.InterestsHeading), nameof(Profile.InterestsSummary),
                nameof(Profile.InterestOneTitle), nameof(Profile.InterestOneDescription), nameof(Profile.InterestTwoTitle),
                nameof(Profile.InterestTwoDescription), nameof(Profile.InterestThreeTitle), nameof(Profile.InterestThreeDescription),
                nameof(Profile.SkillsEyebrow), nameof(Profile.SkillsHeading), nameof(Profile.SkillsSummary), nameof(Profile.Skills),
                nameof(Profile.ContactHeading), nameof(Profile.ContactDescription), nameof(Profile.FooterStatement),
                nameof(Profile.FooterEmail), nameof(Profile.FooterPhone), nameof(Profile.FooterGitHub), nameof(Profile.FooterLinkedIn)
            })
            {
                builder.Property(property).IsRequired().HasMaxLength(2000);
            }
        }
    }
}
