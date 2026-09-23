using System;
using System.ComponentModel.DataAnnotations;

using VelascoPersonalWebsite_IPT.DataAccess.Enums;

namespace VelascoPersonalWebsite_IPT.DataAccess.Entities
{
    public class UserAccount
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [StringLength(254)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public EmploymentStatus EmploymentStatus { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime RegisteredUtc { get; set; }

        public UserAccount()
        {
            UserId = Guid.NewGuid();
            IsActive = true;
            RegisteredUtc = DateTime.UtcNow;
        }
    }
}
