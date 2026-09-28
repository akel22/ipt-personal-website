using System;
using System.Linq;
using VelascoPersonalWebsite_IPT.Application.DTOs;
using VelascoPersonalWebsite_IPT.DataAccess.DbContext;

namespace VelascoPersonalWebsite_IPT.Application.Services
{
    public class AuthenticationService
    {
        private readonly PasswordHasher passwordHasher;
        private readonly AuditService auditService;

        public AuthenticationService() : this(new PasswordHasher(), new AuditService()) { }

        public AuthenticationService(PasswordHasher passwordHasher) : this(passwordHasher, new AuditService())
        {
        }

        public AuthenticationService(PasswordHasher passwordHasher, AuditService auditService)
        {
            this.passwordHasher = passwordHasher;
            this.auditService = auditService;
        }

        public AuthenticatedSessionDto SignIn(string username, string password, DateTime issuedUtc, DateTime expiresUtc)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
            {
                auditService.RecordFailedSignIn(null);
                return null;
            }
            using (var context = WebsiteDbContextFactory.Create())
            {
                var user = context.UserAccounts.SingleOrDefault(account => account.Username == username.Trim());
                if (user == null || !user.IsActive || !passwordHasher.Verify(password, user.PasswordHash))
                {
                    auditService.RecordFailedSignIn(user == null ? (Guid?)null : user.UserId);
                    return null;
                }
               
                user.IsOnline = true;
                context.SaveChanges();
                auditService.RecordSignIn(user.UserId);
                return new AuthenticatedSessionDto { UserId = user.UserId, Role = user.Role, IssuedUtc = issuedUtc, ExpiresUtc = expiresUtc };
            }
        }

        public bool IsActiveUser(Guid userId)
        {
            using (var context = WebsiteDbContextFactory.Create())
                return context.UserAccounts.Any(user => user.UserId == userId && user.IsActive);
        }

        public void MarkOffline(Guid userId)
        {
            using (var context = WebsiteDbContextFactory.Create())
            {
                var user = context.UserAccounts.SingleOrDefault(account => account.UserId == userId);
                if (user != null && user.IsOnline) { user.IsOnline = false; context.SaveChanges(); }
            }
        }
    }
}
