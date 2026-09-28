using System;
using System.Diagnostics;
using VelascoPersonalWebsite_IPT.DataAccess.DbContext;
using VelascoPersonalWebsite_IPT.DataAccess.Entities;
using VelascoPersonalWebsite_IPT.DataAccess.Enums;

namespace VelascoPersonalWebsite_IPT.Application.Services
{
    public class AuditService
    {
        public void RecordRegistration(Guid userId)
        {
            Record(userId, AuditEventType.Registration);
        }

        public void RecordSignIn(Guid userId)
        {
            Record(userId, AuditEventType.SignIn);
        }

        public void RecordFailedSignIn(Guid? userId)
        {
            Record(userId, AuditEventType.SignInFailed);
        }

        public void RecordSignOut(Guid? userId)
        {
            Record(userId, AuditEventType.SignOut);
        }

        public void RecordPageVisit(Guid? userId)
        {
            Record(userId, AuditEventType.PageVisit);
        }

        public void RecordAdminDashboardAccess(Guid? userId)
        {
            Record(userId, AuditEventType.AdminDashboardAccess);
        }

        public void RecordAdminUserDetailsAccess(Guid? userId)
        {
            Record(userId, AuditEventType.AdminUserDetailsAccess);
        }

        public void RecordAdminProfileSave(Guid? userId)
        {
            Record(userId, AuditEventType.AdminProfileSave);
        }

        public void RecordAdminUserStatusChanged(Guid? userId)
        {
            Record(userId, AuditEventType.AdminUserStatusChanged);
        }

        public void RecordAdminUserDeleted(Guid? userId)
        {
            Record(userId, AuditEventType.AdminUserDeleted);
        }

        public void RecordAuditLogsAccess(Guid? userId)
        {
            Record(userId, AuditEventType.AuditLogsAccess);
        }

        public void RecordAuditLogsSearch(Guid? userId)
        {
            Record(userId, AuditEventType.AuditLogsSearch);
        }

        public void RecordAuditLogsFilter(Guid? userId)
        {
            Record(userId, AuditEventType.AuditLogsFilter);
        }

        public void RecordAuditLogsClear(Guid? userId)
        {
            Record(userId, AuditEventType.AuditLogsClear);
        }

        public void RecordUnauthorizedAccess(Guid? userId)
        {
            Record(userId, AuditEventType.UnauthorizedAccess);
        }

        private static void Record(Guid? userId, AuditEventType eventType)
        {
            try
            {
                using (var context = WebsiteDbContextFactory.Create())
                {
                    context.AuditLogs.Add(new AuditLog(userId, eventType));
                    context.SaveChanges();
                }
            }
            catch (Exception exception)
            {
                Trace.TraceError("Audit event persistence failed for {0}: {1}", eventType, exception);
            }
        }
    }
}
