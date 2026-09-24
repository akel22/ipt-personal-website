using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace IPT_VelascoPersonalWebsite
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Login", "login", "~/Pages/Login.aspx");
            routes.MapPageRoute("Register", "register", "~/Pages/Register.aspx");
            routes.MapPageRoute("Home", "home", "~/Home.aspx");
            routes.MapPageRoute("AdminDashboard", "admin", "~/Pages/Admin/Dashboard.aspx");
            routes.MapPageRoute("AdminAuditLogs", "admin/audit-logs", "~/Pages/Admin/AuditLogs.aspx");
            routes.MapPageRoute("AdminProfile", "admin/profile", "~/Pages/Admin/Profile.aspx");
            routes.MapPageRoute("AdminUserDetails", "admin/user-details", "~/Pages/Admin/UserDetails.aspx");
            routes.MapPageRoute("Default", "", "~/Pages/Login.aspx");


            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);
        }
    }
}
