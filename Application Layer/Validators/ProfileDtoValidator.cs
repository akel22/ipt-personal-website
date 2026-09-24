using FluentValidation;
using VelascoPersonalWebsite_IPT.Application.DTOs;

namespace VelascoPersonalWebsite_IPT.Application.Validators
{
    public class ProfileDtoValidator : AbstractValidator<ProfileDto>
    {
        public ProfileDtoValidator()
        {
            RuleFor(dto => dto.GreetingName).NotEmpty().MaximumLength(200);
            RuleFor(dto => dto.Introduction).NotEmpty().MaximumLength(2000);
            RuleFor(dto => dto.Email).NotEmpty().MaximumLength(254).EmailAddress();
            RuleFor(dto => dto.LinkedInUrl).NotEmpty().MaximumLength(500);
            RuleFor(dto => dto.GitHubUrl).NotEmpty().MaximumLength(500);
            RuleFor(dto => dto.EducationHeading).NotEmpty().MaximumLength(200);
            RuleFor(dto => dto.EducationSummary).NotEmpty().MaximumLength(2000);
            RuleFor(dto => dto.CurrentEducationPeriod).NotEmpty().MaximumLength(200);
            RuleFor(dto => dto.CurrentEducationTitle).NotEmpty().MaximumLength(200);
            RuleFor(dto => dto.CurrentEducationDescription).NotEmpty().MaximumLength(2000);
            RuleFor(dto => dto.PreviousEducationPeriod).NotEmpty().MaximumLength(200);
            RuleFor(dto => dto.PreviousEducationTitle).NotEmpty().MaximumLength(200);
            RuleFor(dto => dto.PreviousEducationDescription).NotEmpty().MaximumLength(2000);
            RuleFor(dto => dto.InterestsHeading).NotEmpty().MaximumLength(200);
            RuleFor(dto => dto.InterestsSummary).NotEmpty().MaximumLength(2000);
            RuleFor(dto => dto.InterestOneTitle).NotEmpty().MaximumLength(200);
            RuleFor(dto => dto.InterestOneDescription).NotEmpty().MaximumLength(2000);
            RuleFor(dto => dto.InterestTwoTitle).NotEmpty().MaximumLength(200);
            RuleFor(dto => dto.InterestTwoDescription).NotEmpty().MaximumLength(2000);
            RuleFor(dto => dto.InterestThreeTitle).NotEmpty().MaximumLength(200);
            RuleFor(dto => dto.InterestThreeDescription).NotEmpty().MaximumLength(2000);
            RuleFor(dto => dto.SkillsEyebrow).NotEmpty().MaximumLength(200);
            RuleFor(dto => dto.SkillsHeading).NotEmpty().MaximumLength(200);
            RuleFor(dto => dto.SkillsSummary).NotEmpty().MaximumLength(2000);
            RuleFor(dto => dto.Skills).NotEmpty().MaximumLength(2000);
            RuleFor(dto => dto.ContactHeading).NotEmpty().MaximumLength(200);
            RuleFor(dto => dto.ContactDescription).NotEmpty().MaximumLength(2000);
            RuleFor(dto => dto.FooterStatement).NotEmpty().MaximumLength(200);
            RuleFor(dto => dto.FooterEmail).NotEmpty().MaximumLength(254).EmailAddress();
            RuleFor(dto => dto.FooterPhone).NotEmpty().MaximumLength(100);
            RuleFor(dto => dto.FooterGitHub).NotEmpty().MaximumLength(500);
            RuleFor(dto => dto.FooterLinkedIn).NotEmpty().MaximumLength(500);
        }
    }
}
