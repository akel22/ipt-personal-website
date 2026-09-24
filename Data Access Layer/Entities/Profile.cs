using System;
using System.ComponentModel.DataAnnotations;

namespace VelascoPersonalWebsite_IPT.DataAccess.Entities
{
    public class Profile
    {
        [Key]
        public int ProfileId { get; private set; }

        [Required] public string GreetingName { get; set; }
        [Required] public string Introduction { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string LinkedInUrl { get; set; }
        [Required] public string GitHubUrl { get; set; }
        [Required] public string EducationHeading { get; set; }
        [Required] public string EducationSummary { get; set; }
        [Required] public string CurrentEducationPeriod { get; set; }
        [Required] public string CurrentEducationTitle { get; set; }
        [Required] public string CurrentEducationDescription { get; set; }
        [Required] public string PreviousEducationPeriod { get; set; }
        [Required] public string PreviousEducationTitle { get; set; }
        [Required] public string PreviousEducationDescription { get; set; }
        [Required] public string InterestsHeading { get; set; }
        [Required] public string InterestsSummary { get; set; }
        [Required] public string InterestOneTitle { get; set; }
        [Required] public string InterestOneDescription { get; set; }
        [Required] public string InterestTwoTitle { get; set; }
        [Required] public string InterestTwoDescription { get; set; }
        [Required] public string InterestThreeTitle { get; set; }
        [Required] public string InterestThreeDescription { get; set; }
        [Required] public string SkillsEyebrow { get; set; }
        [Required] public string SkillsHeading { get; set; }
        [Required] public string SkillsSummary { get; set; }
        [Required] public string Skills { get; set; }
        [Required] public string ContactHeading { get; set; }
        [Required] public string ContactDescription { get; set; }
        [Required] public string FooterStatement { get; set; }
        [Required] public string FooterEmail { get; set; }
        [Required] public string FooterPhone { get; set; }
        [Required] public string FooterGitHub { get; set; }
        [Required] public string FooterLinkedIn { get; set; }

        public DateTime UpdatedUtc { get; set; }

        public Profile()
        {
            UpdatedUtc = DateTime.UtcNow;
        }
    }
}
