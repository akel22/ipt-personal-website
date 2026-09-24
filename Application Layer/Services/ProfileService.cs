using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluentValidation.Results;
using VelascoPersonalWebsite_IPT.Application.DTOs;
using VelascoPersonalWebsite_IPT.Application.Validators;
using VelascoPersonalWebsite_IPT.DataAccess.DbContext;
using VelascoPersonalWebsite_IPT.DataAccess.Entities;
using VelascoPersonalWebsite_IPT.DataAccess.Enums;

namespace VelascoPersonalWebsite_IPT.Application.Services
{
    public class ProfileService
    {
        private readonly ProfileDtoValidator validator = new ProfileDtoValidator();

        public ProfileDto GetOrCreate()
        {
            using (var context = WebsiteDbContextFactory.Create())
            {
                var profile = context.Profiles.SingleOrDefault();
                if (profile == null)
                {
                    profile = CreateDefaultProfile();
                    context.Profiles.Add(profile);
                    context.SaveChanges();
                }

                return ToDto(profile);
            }
        }

    public class DashboardStatisticsService
    {
        public DashboardStatisticsDto GetStatistics()
        {
            using (var context = WebsiteDbContextFactory.Create())
            {
                var users = context.UserAccounts
                    .Select(user => new
                    {
                        user.IsActive,
                        user.IsOnline,
                        user.EmploymentStatus,
                        user.RegisteredUtc
                    })
                    .ToList();

                var currentMonth = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
                var previousMonth = currentMonth.AddMonths(-1);
                var chartLabels = new List<string>();
                var chartValues = new List<int>();

                for (var monthOffset = 5; monthOffset >= 0; monthOffset--)
                {
                    var month = currentMonth.AddMonths(-monthOffset);
                    var nextMonth = month.AddMonths(1);
                    chartLabels.Add(month.ToString("MMMM", CultureInfo.InvariantCulture));
                    chartValues.Add(users.Count(user => user.RegisteredUtc >= month && user.RegisteredUtc < nextMonth));
                }

                var totalRegistered = users.Count;
                var studentCount = users.Count(user => user.EmploymentStatus == EmploymentStatus.Student);
                var employedCount = users.Count(user => user.EmploymentStatus == EmploymentStatus.Employed);
                var unemployedCount = users.Count(user => user.EmploymentStatus == EmploymentStatus.Unemployed);

                return new DashboardStatisticsDto
                {
                    TotalRegistered = totalRegistered,
                    ActiveMembers = users.Count(user => user.IsActive),
                    ActiveNow = users.Count(user => user.IsOnline),
                    RegistrationTrendPercentage = CalculatePercentageChange(
                        users.Count(user => user.RegisteredUtc >= previousMonth && user.RegisteredUtc < currentMonth),
                        users.Count(user => user.RegisteredUtc >= currentMonth && user.RegisteredUtc < currentMonth.AddMonths(1))),
                    RegistrationChartLabels = chartLabels,
                    RegistrationChartValues = chartValues,
                    StudentPercentage = CalculatePercentage(studentCount, totalRegistered),
                    EmployedPercentage = CalculatePercentage(employedCount, totalRegistered),
                    UnemployedPercentage = CalculatePercentage(unemployedCount, totalRegistered)
                };
            }
        }

        private static int CalculatePercentage(int count, int total)
        {
            return total == 0 ? 0 : (int)Math.Round(count * 100m / total, MidpointRounding.AwayFromZero);
        }

        private static decimal CalculatePercentageChange(int previousCount, int currentCount)
        {
            if (previousCount == 0)
            {
                return currentCount == 0 ? 0 : 100;
            }

            return Math.Round((currentCount - previousCount) * 100m / previousCount, 1, MidpointRounding.AwayFromZero);
        }
    }
        

        public ValidationResult Save(ProfileDto dto)
        {
            var validation = validator.Validate(dto);
            if (!validation.IsValid)
            {
                return validation;
            }

            using (var context = WebsiteDbContextFactory.Create())
            {
                var profile = context.Profiles.SingleOrDefault() ?? new Profile();
                Apply(dto, profile);
                profile.UpdatedUtc = System.DateTime.UtcNow;
                if (profile.ProfileId == 0)
                {
                    context.Profiles.Add(profile);
                }

                context.SaveChanges();
            }

            return validation;
        }

        private static ProfileDto ToDto(Profile profile)
        {
            return new ProfileDto
            {
                ProfileId = profile.ProfileId, GreetingName = profile.GreetingName, Introduction = profile.Introduction,
                Email = profile.Email, LinkedInUrl = profile.LinkedInUrl, GitHubUrl = profile.GitHubUrl,
                EducationHeading = profile.EducationHeading, EducationSummary = profile.EducationSummary,
                CurrentEducationPeriod = profile.CurrentEducationPeriod, CurrentEducationTitle = profile.CurrentEducationTitle,
                CurrentEducationDescription = profile.CurrentEducationDescription, PreviousEducationPeriod = profile.PreviousEducationPeriod,
                PreviousEducationTitle = profile.PreviousEducationTitle, PreviousEducationDescription = profile.PreviousEducationDescription,
                InterestsHeading = profile.InterestsHeading, InterestsSummary = profile.InterestsSummary,
                InterestOneTitle = profile.InterestOneTitle, InterestOneDescription = profile.InterestOneDescription,
                InterestTwoTitle = profile.InterestTwoTitle, InterestTwoDescription = profile.InterestTwoDescription,
                InterestThreeTitle = profile.InterestThreeTitle, InterestThreeDescription = profile.InterestThreeDescription,
                SkillsEyebrow = profile.SkillsEyebrow, SkillsHeading = profile.SkillsHeading, SkillsSummary = profile.SkillsSummary,
                Skills = profile.Skills, ContactHeading = profile.ContactHeading, ContactDescription = profile.ContactDescription,
                FooterStatement = profile.FooterStatement, FooterEmail = profile.FooterEmail, FooterPhone = profile.FooterPhone,
                FooterGitHub = profile.FooterGitHub, FooterLinkedIn = profile.FooterLinkedIn
            };
        }

        private static void Apply(ProfileDto dto, Profile profile)
        {
            profile.GreetingName = dto.GreetingName.Trim(); profile.Introduction = dto.Introduction.Trim(); profile.Email = dto.Email.Trim();
            profile.LinkedInUrl = dto.LinkedInUrl.Trim(); profile.GitHubUrl = dto.GitHubUrl.Trim(); profile.EducationHeading = dto.EducationHeading.Trim();
            profile.EducationSummary = dto.EducationSummary.Trim(); profile.CurrentEducationPeriod = dto.CurrentEducationPeriod.Trim();
            profile.CurrentEducationTitle = dto.CurrentEducationTitle.Trim(); profile.CurrentEducationDescription = dto.CurrentEducationDescription.Trim();
            profile.PreviousEducationPeriod = dto.PreviousEducationPeriod.Trim(); profile.PreviousEducationTitle = dto.PreviousEducationTitle.Trim();
            profile.PreviousEducationDescription = dto.PreviousEducationDescription.Trim(); profile.InterestsHeading = dto.InterestsHeading.Trim();
            profile.InterestsSummary = dto.InterestsSummary.Trim(); profile.InterestOneTitle = dto.InterestOneTitle.Trim();
            profile.InterestOneDescription = dto.InterestOneDescription.Trim(); profile.InterestTwoTitle = dto.InterestTwoTitle.Trim();
            profile.InterestTwoDescription = dto.InterestTwoDescription.Trim(); profile.InterestThreeTitle = dto.InterestThreeTitle.Trim();
            profile.InterestThreeDescription = dto.InterestThreeDescription.Trim(); profile.SkillsEyebrow = dto.SkillsEyebrow.Trim();
            profile.SkillsHeading = dto.SkillsHeading.Trim(); profile.SkillsSummary = dto.SkillsSummary.Trim(); profile.Skills = dto.Skills.Trim();
            profile.ContactHeading = dto.ContactHeading.Trim(); profile.ContactDescription = dto.ContactDescription.Trim(); profile.FooterStatement = dto.FooterStatement.Trim();
            profile.FooterEmail = dto.FooterEmail.Trim(); profile.FooterPhone = dto.FooterPhone.Trim(); profile.FooterGitHub = dto.FooterGitHub.Trim();
            profile.FooterLinkedIn = dto.FooterLinkedIn.Trim();
        }

        private static Profile CreateDefaultProfile()
        {
            return new Profile
            {
                GreetingName = "EJ", Introduction = "An Information Technology student aspiring to be a Backend/ Data Engineer.",
                Email = "velasco.ezekieljohn.javellana@gmail.com", LinkedInUrl = "https://www.linkedin.com/in/ezekiel-john-velasco-2a15a2417/", GitHubUrl = "https://github.com/akel22",
                EducationHeading = "Educational attainment", EducationSummary = "The milestones that continue to shape my path in technology.",
                CurrentEducationPeriod = "2024 - Present", CurrentEducationTitle = "Information Technology", CurrentEducationDescription = "Currently pursuing an Information Technology degree.",
                PreviousEducationPeriod = "2022 - 2024", PreviousEducationTitle = "Senior High School Graduate", PreviousEducationDescription = "Completed a Science, Technology, Engineering, and Mathematics (STEM) strand.",
                InterestsHeading = "Hobbies and interests", InterestsSummary = "A few simple things that keep me curious, focused, and inspired.",
                InterestOneTitle = "Studying", InterestOneDescription = "Learning something new every day.", InterestTwoTitle = "Music", InterestTwoDescription = "Listening to music and discovering new sounds.",
                InterestThreeTitle = "Movies", InterestThreeDescription = "Watching movies and exploring different stories.", SkillsEyebrow = "Skills & experience", SkillsHeading = "Working with technologies and tools",
                SkillsSummary = "A growing toolkit for building reliable applications, data solutions, and thoughtful digital experiences.", Skills = "Python|C#|Java|JavaScript|TypeScript|HTML|CSS|ASP.NET|Django|PostgreSQL|MongoDB",
                ContactHeading = "Get in touch", ContactDescription = "Have a question, opportunity, or idea to share? Send me a message and I will get back to you.",
                FooterStatement = "Just keep|learning.", FooterEmail = "velasco.ezekieljohn.javellana@gmail.com", FooterPhone = "+63 928-494-8326", FooterGitHub = "GitHub: akel22", FooterLinkedIn = "LinkedIn: Ezekiel John Velasco"
            };
        }
    }
}
