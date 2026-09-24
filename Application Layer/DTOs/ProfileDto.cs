using System.Collections.Generic;

namespace VelascoPersonalWebsite_IPT.Application.DTOs
{
    public class ProfileDto
    {
        public int ProfileId { get; set; }
        public string GreetingName { get; set; }
        public string Introduction { get; set; }
        public string Email { get; set; }
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        public string EducationHeading { get; set; }
        public string EducationSummary { get; set; }
        public string CurrentEducationPeriod { get; set; }
        public string CurrentEducationTitle { get; set; }
        public string CurrentEducationDescription { get; set; }
        public string PreviousEducationPeriod { get; set; }
        public string PreviousEducationTitle { get; set; }
        public string PreviousEducationDescription { get; set; }
        public string InterestsHeading { get; set; }
        public string InterestsSummary { get; set; }
        public string InterestOneTitle { get; set; }
        public string InterestOneDescription { get; set; }
        public string InterestTwoTitle { get; set; }
        public string InterestTwoDescription { get; set; }
        public string InterestThreeTitle { get; set; }
        public string InterestThreeDescription { get; set; }
        public string SkillsEyebrow { get; set; }
        public string SkillsHeading { get; set; }
        public string SkillsSummary { get; set; }
        public string Skills { get; set; }
        public string ContactHeading { get; set; }
        public string ContactDescription { get; set; }
        public string FooterStatement { get; set; }
        public string FooterEmail { get; set; }
        public string FooterPhone { get; set; }
        public string FooterGitHub { get; set; }
        public string FooterLinkedIn { get; set; }
    }

    public class DashboardStatisticsDto
    {
        public int TotalRegistered { get; set; }
        public int ActiveMembers { get; set; }
        public int ActiveNow { get; set; }
        public decimal RegistrationTrendPercentage { get; set; }
        public IList<string> RegistrationChartLabels { get; set; }
        public IList<int> RegistrationChartValues { get; set; }
        public int StudentPercentage { get; set; }
        public int EmployedPercentage { get; set; }
        public int UnemployedPercentage { get; set; }
    }
}
