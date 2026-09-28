using System;
using System.Web.UI;

namespace VelascoPersonalWebsite_IPT
{
    public partial class AdminMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User == null || !Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect(ResolveUrl("~/login"), false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!Context.User.IsInRole("Admin"))
            {
                Response.Redirect(ResolveUrl("~/home"), false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            var currentPath = Request.Url.AbsolutePath.TrimEnd('/');
            var dashboardPath = ResolveUrl("~/admin").TrimEnd('/');
            var auditLogsPath = ResolveUrl("~/admin/audit-logs").TrimEnd('/');
            var profilePath = ResolveUrl("~/admin/profile").TrimEnd('/');

            SetActiveLink(DashboardLink, currentPath == dashboardPath);
            SetActiveLink(AuditLogsLink, currentPath == auditLogsPath);
            SetActiveLink(ProfileLink, currentPath == profilePath);
        }

        private static void SetActiveLink(System.Web.UI.WebControls.HyperLink link, bool isActive)
        {
            link.CssClass = isActive
                ? "admin-sidebar-link active"
                : "admin-sidebar-link";

            if (isActive)
            {
                link.Attributes["aria-current"] = "page";
            }
        }
    }
}
