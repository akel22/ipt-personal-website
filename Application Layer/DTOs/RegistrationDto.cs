using System;

using VelascoPersonalWebsite_IPT.DataAccess.Enums;

namespace VelascoPersonalWebsite_IPT.Application.DTOs
{
    public class RegistrationDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public EmploymentStatus EmploymentStatus { get; set; }

        public Gender Gender { get; set; }
    }
}
