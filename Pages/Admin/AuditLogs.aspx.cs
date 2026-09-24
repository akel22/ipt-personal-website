using System;
using System.Linq;
using System.Web.UI;
using Microsoft.EntityFrameworkCore;
using VelascoPersonalWebsite_IPT.DataAccess.DbContext;

namespace VelascoPersonalWebsite_IPT.Pages.Admin
{
    public partial class AuditLogs : Page
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

            BindAuditLogs();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            BindAuditLogs();
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            BindAuditLogs();
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            UserSearchTextBox.Text = string.Empty;
            EventTypeDropDownList.SelectedValue = "All";
            BindAuditLogs();
        }

        protected string FormatEventType(object value)
        {
            return value == null ? string.Empty : value.ToString() == "SignIn" ? "Sign in" : value.ToString();
        }

        private void BindAuditLogs()
        {
            using (var context = WebsiteDbContextFactory.Create())
            {
                var search = UserSearchTextBox.Text.Trim();
                var eventType = EventTypeDropDownList.SelectedValue;
                var query = context.AuditLogs.Include(log => log.User).AsQueryable();
                if (!string.IsNullOrWhiteSpace(search)) query = query.Where(log => log.User.Username.Contains(search) || log.User.Email.Contains(search));
                if (eventType != "All") query = query.Where(log => log.EventType.ToString() == eventType);
                var results = query.OrderByDescending(log => log.OccurredUtc).Take(200).ToList();
                AuditLogsRepeater.DataSource = results;
                AuditLogsRepeater.DataBind();
                FeedbackLiteral.Text = results.Count == 0 ? "<div class=\"p-4 text-center small text-secondary\">No audit logs matched your search.</div>" : string.Empty;
            }
        }
    }
}
