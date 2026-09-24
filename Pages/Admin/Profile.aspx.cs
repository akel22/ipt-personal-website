using System;
using System.Linq;
using System.Web.UI;
using VelascoPersonalWebsite_IPT.Application.DTOs;
using VelascoPersonalWebsite_IPT.Application.Services;

namespace VelascoPersonalWebsite_IPT.Pages.Admin
{
    public partial class Profile : Page
    {
        private readonly ProfileService profileService = new ProfileService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Populate(profileService.GetOrCreate());
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            var result = profileService.Save(ReadForm());
            if (!result.IsValid)
            {
                FeedbackLiteral.Text = "<div class=\"alert alert-danger\">Please correct the highlighted profile values.</div>";
                ProfileValidationSummary.HeaderText = string.Join(" ", result.Errors.Select(error => Server.HtmlEncode(error.ErrorMessage)));
                return;
            }

            FeedbackLiteral.Text = "<div class=\"alert alert-success\">Profile saved successfully.</div>";
        }

        private ProfileDto ReadForm()
        {
            return new ProfileDto
            {
                GreetingName = GreetingNameTextBox.Text, Introduction = IntroductionTextBox.Text, Email = EmailTextBox.Text,
                LinkedInUrl = LinkedInUrlTextBox.Text, GitHubUrl = GitHubUrlTextBox.Text, EducationHeading = EducationHeadingTextBox.Text,
                EducationSummary = EducationSummaryTextBox.Text, CurrentEducationPeriod = CurrentEducationPeriodTextBox.Text,
                CurrentEducationTitle = CurrentEducationTitleTextBox.Text, CurrentEducationDescription = CurrentEducationDescriptionTextBox.Text,
                PreviousEducationPeriod = PreviousEducationPeriodTextBox.Text, PreviousEducationTitle = PreviousEducationTitleTextBox.Text,
                PreviousEducationDescription = PreviousEducationDescriptionTextBox.Text, InterestsHeading = InterestsHeadingTextBox.Text,
                InterestsSummary = InterestsSummaryTextBox.Text, InterestOneTitle = InterestOneTitleTextBox.Text,
                InterestOneDescription = InterestOneDescriptionTextBox.Text, InterestTwoTitle = InterestTwoTitleTextBox.Text,
                InterestTwoDescription = InterestTwoDescriptionTextBox.Text, InterestThreeTitle = InterestThreeTitleTextBox.Text,
                InterestThreeDescription = InterestThreeDescriptionTextBox.Text, SkillsEyebrow = SkillsEyebrowTextBox.Text,
                SkillsHeading = SkillsHeadingTextBox.Text, SkillsSummary = SkillsSummaryTextBox.Text, Skills = SkillsTextBox.Text,
                ContactHeading = ContactHeadingTextBox.Text, ContactDescription = ContactDescriptionTextBox.Text,
                FooterStatement = FooterStatementTextBox.Text, FooterEmail = FooterEmailTextBox.Text, FooterPhone = FooterPhoneTextBox.Text,
                FooterGitHub = FooterGitHubTextBox.Text, FooterLinkedIn = FooterLinkedInTextBox.Text
            };
        }

        private void Populate(ProfileDto profile)
        {
            GreetingNameTextBox.Text = profile.GreetingName; IntroductionTextBox.Text = profile.Introduction; EmailTextBox.Text = profile.Email;
            LinkedInUrlTextBox.Text = profile.LinkedInUrl; GitHubUrlTextBox.Text = profile.GitHubUrl; EducationHeadingTextBox.Text = profile.EducationHeading;
            EducationSummaryTextBox.Text = profile.EducationSummary; CurrentEducationPeriodTextBox.Text = profile.CurrentEducationPeriod;
            CurrentEducationTitleTextBox.Text = profile.CurrentEducationTitle; CurrentEducationDescriptionTextBox.Text = profile.CurrentEducationDescription;
            PreviousEducationPeriodTextBox.Text = profile.PreviousEducationPeriod; PreviousEducationTitleTextBox.Text = profile.PreviousEducationTitle;
            PreviousEducationDescriptionTextBox.Text = profile.PreviousEducationDescription; InterestsHeadingTextBox.Text = profile.InterestsHeading;
            InterestsSummaryTextBox.Text = profile.InterestsSummary; InterestOneTitleTextBox.Text = profile.InterestOneTitle;
            InterestOneDescriptionTextBox.Text = profile.InterestOneDescription; InterestTwoTitleTextBox.Text = profile.InterestTwoTitle;
            InterestTwoDescriptionTextBox.Text = profile.InterestTwoDescription; InterestThreeTitleTextBox.Text = profile.InterestThreeTitle;
            InterestThreeDescriptionTextBox.Text = profile.InterestThreeDescription; SkillsEyebrowTextBox.Text = profile.SkillsEyebrow;
            SkillsHeadingTextBox.Text = profile.SkillsHeading; SkillsSummaryTextBox.Text = profile.SkillsSummary; SkillsTextBox.Text = profile.Skills;
            ContactHeadingTextBox.Text = profile.ContactHeading; ContactDescriptionTextBox.Text = profile.ContactDescription;
            FooterStatementTextBox.Text = profile.FooterStatement; FooterEmailTextBox.Text = profile.FooterEmail; FooterPhoneTextBox.Text = profile.FooterPhone;
            FooterGitHubTextBox.Text = profile.FooterGitHub; FooterLinkedInTextBox.Text = profile.FooterLinkedIn;
        }
    }
}
