using System;
using System.Linq;
using VelascoPersonalWebsite_IPT.Application.DTOs;
using VelascoPersonalWebsite_IPT.DataAccess.DbContext;

namespace VelascoPersonalWebsite_IPT.Application.Services
{
    public class AuthenticationService
    {
        private readonly PasswordHasher passwordHasher;

        public AuthenticationService() : this(new PasswordHasher()) { }

        public AuthenticationService(PasswordHasher passwordHasher)
        {
            this.passwordHasher = passwordHasher;
        }

        public AuthenticatedSessionDto SignIn(string username, string password, DateTime issuedUtc, DateTime expiresUtc)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password)) return null;
            using (var context = WebsiteDbContextFactory.Create())
            {
                var user = context.UserAccounts.SingleOrDefault(account => account.Username == username.Trim());
                if (user == null || !user.IsActive || !passwordHasher.Verify(password, user.PasswordHash)) return null;
                user.IsOnline = true;
                context.SaveChanges();
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
