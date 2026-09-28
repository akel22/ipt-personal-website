using System;
using System.Linq;
using VelascoPersonalWebsite_IPT.DataAccess.DbContext;

namespace VelascoPersonalWebsite_IPT.Application.Services
{
    public class UserAccountService
    {
        public bool ToggleActive(Guid userId)
        {
            using (var context = WebsiteDbContextFactory.Create())
            {
                var user = context.UserAccounts.Find(userId);
                if (user == null)
                {
                    return false;
                }

                user.IsActive = !user.IsActive;
                context.SaveChanges();
                return true;
            }
        }

        public bool Delete(Guid userId)
        {
            using (var context = WebsiteDbContextFactory.Create())
            {
                var user = context.UserAccounts.Find(userId);
                if (user == null)
                {
                    return false;
                }

                var auditLogs = context.AuditLogs.Where(log => log.UserId == userId).ToList();
                context.AuditLogs.RemoveRange(auditLogs);
                context.UserAccounts.Remove(user);
                context.SaveChanges();
                return true;
            }
        }
    }
}
