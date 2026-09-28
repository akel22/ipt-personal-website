namespace VelascoPersonalWebsite_IPT.DataAccess.Enums
{
    public enum AuditEventType
    {
        Registration = 1,
        SignIn = 2,
        SignInFailed = 3,
        SignOut = 4,
        PageVisit = 5,
        AdminDashboardAccess = 6,
        AdminUserDetailsAccess = 7,
        AdminProfileSave = 8,
        AdminUserStatusChanged = 9,
        AdminUserDeleted = 10,
        AuditLogsAccess = 11,
        AuditLogsSearch = 12,
        AuditLogsFilter = 13,
        AuditLogsClear = 14,
        UnauthorizedAccess = 15
    }
}
