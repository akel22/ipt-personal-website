using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using VelascoPersonalWebsite_IPT.Application.DTOs;
using VelascoPersonalWebsite_IPT.DataAccess.DbContext;
using VelascoPersonalWebsite_IPT.DataAccess.Entities;
using VelascoPersonalWebsite_IPT.DataAccess.Enums;

namespace VelascoPersonalWebsite_IPT.Application.Services
{
    public class RegistrationService
    {
        private readonly PasswordHasher passwordHasher;
        private readonly AuditService auditService;

        public RegistrationService() : this(new PasswordHasher(), new AuditService()) { }

        public RegistrationService(PasswordHasher hasher) : this(hasher, new AuditService())
        {
        }

        public RegistrationService(PasswordHasher hasher, AuditService auditService)
        {
            passwordHasher = hasher;
            this.auditService = auditService;
        }

        public bool Register(RegistrationDto dto, out string error)
        {
            error = null;

            if (dto == null)
            {
                error = "registration-invalid";
                return false;
            }

            // Basic validations
            if (string.IsNullOrWhiteSpace(dto.Username) || dto.Username.Length < 3 || dto.Username.Length > 20)
            {
                error = "username-invalid";
                return false;
            }

            var usernameRegex = new Regex("^[A-Za-z0-9_]{3,20}$");
            if (!usernameRegex.IsMatch(dto.Username))
            {
                error = "username-invalid";
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.Email))
            {
                error = "email-invalid";
                return false;
            }

            var emailRegex = new Regex("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$");
            if (!emailRegex.IsMatch(dto.Email))
            {
                error = "email-invalid";
                return false;
            }

            if (string.IsNullOrEmpty(dto.Password) || dto.Password.Length < 8)
            {
                error = "password-invalid";
                return false;
            }

            // Password complexity: at least one lowercase, uppercase, digit and special char
            var pwdRegex = new Regex("(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^A-Za-z\\d]).{8,}");
            if (!pwdRegex.IsMatch(dto.Password))
            {
                error = "password-invalid";
                return false;
            }

            if (!string.Equals(dto.Password, dto.ConfirmPassword, StringComparison.Ordinal))
            {
                error = "password-mismatch";
                return false;
            }

            if (!dto.DateOfBirth.HasValue)
            {
                error = "date-invalid";
                return false;
            }

            if (dto.DateOfBirth.Value > DateTime.UtcNow)
            {
                error = "date-invalid";
                return false;
            }

            if (!Enum.IsDefined(typeof(EmploymentStatus), dto.EmploymentStatus) || dto.EmploymentStatus == 0 ||
                !Enum.IsDefined(typeof(Gender), dto.Gender) || dto.Gender == 0)
            {
                error = "profile-invalid";
                return false;
            }

            // Check uniqueness and create user
            try
            {
                using (var context = WebsiteDbContextFactory.Create())
                {
                    var exists = context.UserAccounts.Any(u => u.Username == dto.Username || u.Email == dto.Email);
                    if (exists)
                    {
                        error = "conflict";
                        return false;
                    }

                    var account = new UserAccount
                    {
                        Username = dto.Username,
                        Email = dto.Email,
                        PasswordHash = passwordHasher.Hash(dto.Password),
                        DateOfBirth = dto.DateOfBirth.Value,
                        EmploymentStatus = dto.EmploymentStatus,
                        Gender = dto.Gender
                    };

                    context.UserAccounts.Add(account);
                    context.SaveChanges();
                    auditService.RecordRegistration(account.UserId);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError("Registration persistence failed: {0}", ex);
                error = "error";
                return false;
            }
        }
    }
}
