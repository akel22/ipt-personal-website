using System;

using VelascoPersonalWebsite_IPT.DataAccess.Enums;

namespace VelascoPersonalWebsite_IPT.Application.DTOs
{
    public class UserAccountDto
    {
        public Guid UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }

        public EmploymentStatus EmploymentStatus { get; set; }

        public Gender Gender { get; set; }

        public bool IsActive { get; set; }

        public DateTime RegisteredUtc { get; set; }
    }
}
