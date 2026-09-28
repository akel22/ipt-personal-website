using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using VelascoPersonalWebsite_IPT.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using VelascoPersonalWebsite_IPT.Application.Services;
using VelascoPersonalWebsite_IPT.DataAccess.DbContext;
using VelascoPersonalWebsite_IPT.DataAccess.Enums;

namespace VelascoPersonalWebsite_IPT.Pages.Admin
{
    public partial class AuditLogs : Page
    {
        private const int AuditLogsPageSize = 8;
        private readonly AuditService auditService = new AuditService();

        protected int AuditLogsCurrentPage
        {
            get { return ViewState["AuditLogsCurrentPage"] == null ? 1 : (int)ViewState["AuditLogsCurrentPage"]; }
            set { ViewState["AuditLogsCurrentPage"] = value; }
        }

        protected int AuditLogsTotalPages { get; private set; }

        private static List<PaginationItem> BuildPagination(int totalPages, int currentPage)
        {
            var items = new List<PaginationItem>();
            if (totalPages <= 1) return items;

            Action<int> addPage = page => items.Add(new PaginationItem
            {
                PageNumber = page,
                Text = page.ToString(),
                IsCurrent = page == currentPage
            });

            if (totalPages <= 7)
            {
                for (var page = 1; page <= totalPages; page++) addPage(page);
                return items;
            }

            addPage(1);
            if (currentPage > 4) items.Add(new PaginationItem { Text = "…", IsEllipsis = true });
            var start = Math.Max(2, currentPage - 1);
            var end = Math.Min(totalPages - 1, currentPage + 1);
            for (var page = start; page <= end; page++) addPage(page);
            if (currentPage < totalPages - 3) items.Add(new PaginationItem { Text = "…", IsEllipsis = true });
            addPage(totalPages);
            return items;
        }

        protected string GetPaginationCss(PaginationItem item)
        {
            return item.IsCurrent ? " active" : item.IsEllipsis ? " disabled" : string.Empty;
        }

        protected string FormatAuditUser(object value)
        {
            var user = value as VelascoPersonalWebsite_IPT.DataAccess.Entities.UserAccount;
            return user == null ? "Anonymous/system" : user.Username;
        }

        protected string FormatAuditEmail(object value)
        {
            var user = value as VelascoPersonalWebsite_IPT.DataAccess.Entities.UserAccount;
            return user == null ? string.Empty : user.Email;
        }
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

            RecordAuditAccess();
            BindAuditLogs();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            AuditLogsCurrentPage = 1;
            RecordAuditSearch();
            BindAuditLogs();
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            AuditLogsCurrentPage = 1;
            RecordAuditFilter();
            BindAuditLogs();
        }

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            RecordAuditClear();
            UserSearchTextBox.Text = string.Empty;
            EventTypeDropDownList.SelectedValue = "All";
            AuditLogsCurrentPage = 1;
            BindAuditLogs();
        }

        protected string FormatEventType(object value)
        {
            if (value == null) return string.Empty;
            switch (value.ToString())
            {
                case "SignIn": return "Sign in";
                case "SignInFailed": return "Sign-in failed";
                case "SignOut": return "Sign out";
                case "PageVisit": return "Page visit";
                case "AdminDashboardAccess": return "Admin dashboard access";
                case "AdminUserDetailsAccess": return "Admin user details access";
                case "AdminProfileSave": return "Admin profile save";
                case "AdminUserStatusChanged": return "Admin user status changed";
                case "AdminUserDeleted": return "Admin user deleted";
                case "AuditLogsAccess": return "Audit logs access";
                case "AuditLogsSearch": return "Audit logs search";
                case "AuditLogsFilter": return "Audit logs filter";
                case "AuditLogsClear": return "Audit logs clear";
                case "UnauthorizedAccess": return "Unauthorized access";
                default: return value.ToString();
            }
        }

        private void BindAuditLogs()
        {
            using (var context = WebsiteDbContextFactory.Create())
            {
                var search = UserSearchTextBox.Text.Trim();
                var eventType = EventTypeDropDownList.SelectedValue;
                var query = context.AuditLogs.Include(log => log.User).AsQueryable();
                if (!string.IsNullOrWhiteSpace(search)) query = query.Where(log => log.User != null && (log.User.Username.Contains(search) || log.User.Email.Contains(search)));
                AuditEventType parsedEventType;
                if (Enum.TryParse(eventType, out parsedEventType) && Enum.IsDefined(typeof(AuditEventType), parsedEventType))
                    query = query.Where(log => log.EventType == parsedEventType);

                var totalLogs = query.Count();
                AuditLogsTotalPages = Math.Max(1, (int)Math.Ceiling(totalLogs / (double)AuditLogsPageSize));
                if (AuditLogsCurrentPage > AuditLogsTotalPages) AuditLogsCurrentPage = AuditLogsTotalPages;
                AuditLogsPreviousButton.Enabled = AuditLogsCurrentPage > 1;
                AuditLogsNextButton.Enabled = AuditLogsCurrentPage < AuditLogsTotalPages;
                var results = query.OrderByDescending(log => log.OccurredUtc)
                    .Skip((AuditLogsCurrentPage - 1) * AuditLogsPageSize)
                    .Take(AuditLogsPageSize)
                    .ToList();
                AuditLogsRepeater.DataSource = results;
                AuditLogsRepeater.DataBind();
                AuditLogsPaginationRepeater.DataSource = BuildPagination(AuditLogsTotalPages, AuditLogsCurrentPage);
                AuditLogsPaginationRepeater.DataBind();
                FeedbackLiteral.Text = results.Count == 0 ? "<div class=\"p-4 text-center small text-secondary\">No audit logs matched your search.</div>" : string.Empty;
            }
        }

        private void RecordAuditAccess()
        {
            Guid userId;
            if (Guid.TryParse(Context.User.Identity.Name, out userId)) auditService.RecordAuditLogsAccess(userId);
        }

        private void RecordAuditSearch()
        {
            Guid userId;
            if (Guid.TryParse(Context.User.Identity.Name, out userId)) auditService.RecordAuditLogsSearch(userId);
        }

        private void RecordAuditFilter()
        {
            Guid userId;
            if (Guid.TryParse(Context.User.Identity.Name, out userId)) auditService.RecordAuditLogsFilter(userId);
        }

        private void RecordAuditClear()
        {
            Guid userId;
            if (Guid.TryParse(Context.User.Identity.Name, out userId)) auditService.RecordAuditLogsClear(userId);
        }

        protected void AuditLogsPaginationRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "Page") return;
            int page;
            if (!int.TryParse(Convert.ToString(e.CommandArgument), out page)) return;
            ResetAuditFilters();
            AuditLogsCurrentPage = Math.Max(1, page);
            BindAuditLogs();
        }

        protected void AuditLogsPreviousButton_Click(object sender, EventArgs e)
        {
            ResetAuditFilters();
            AuditLogsCurrentPage = Math.Max(1, AuditLogsCurrentPage - 1);
            BindAuditLogs();
        }

        protected void AuditLogsNextButton_Click(object sender, EventArgs e)
        {
            ResetAuditFilters();
            AuditLogsCurrentPage++;
            BindAuditLogs();
        }

        private void ResetAuditFilters()
        {
            UserSearchTextBox.Text = string.Empty;
            EventTypeDropDownList.SelectedValue = "All";
        }
    }
}
