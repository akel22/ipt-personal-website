using System;
using FluentValidation;

using VelascoPersonalWebsite_IPT.Application.DTOs;
using VelascoPersonalWebsite_IPT.DataAccess.Enums;

namespace VelascoPersonalWebsite_IPT.Application.Validators
{
    public class RegistrationDtoValidator : AbstractValidator<RegistrationDto>
    {
        private const string UsernamePattern = "^[A-Za-z0-9_]+$";
        private const string PasswordPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^A-Za-z\\d]).{8,}$";

        public RegistrationDtoValidator()
        {
            RuleFor(dto => dto.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(3, 20)
                .Matches(UsernamePattern);

            RuleFor(dto => dto.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(254)
                .EmailAddress();

            RuleFor(dto => dto.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Matches(PasswordPattern);

            RuleFor(dto => dto.ConfirmPassword)
                .NotEmpty()
                .Equal(dto => dto.Password);

            RuleFor(dto => dto.DateOfBirth)
                .NotNull()
                .Must(dateOfBirth => dateOfBirth.Value.Date < DateTime.UtcNow.Date &&
                      dateOfBirth.Value.Date > DateTime.UtcNow.AddYears(-10).Date)
                .When(dto => dto.DateOfBirth.HasValue);

            RuleFor(dto => dto.EmploymentStatus)
                .IsInEnum()
                .NotEqual(default(EmploymentStatus));

            RuleFor(dto => dto.Gender)
                .IsInEnum()
                .NotEqual(default(Gender));
        }
    }
}
