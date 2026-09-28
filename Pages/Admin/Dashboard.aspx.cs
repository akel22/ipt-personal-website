using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Linq;
using VelascoPersonalWebsite_IPT.Application.DTOs;
using VelascoPersonalWebsite_IPT.Application.Services;
using VelascoPersonalWebsite_IPT.DataAccess.DbContext;
using VelascoPersonalWebsite_IPT.DataAccess.Enums;

namespace VelascoPersonalWebsite_IPT.Pages.Admin
{
    public class PaginationItem
    {
        public int PageNumber { get; set; }
        public string Text { get; set; }
        public bool IsCurrent { get; set; }
        public bool IsEllipsis { get; set; }
    }

    public partial class Dashboard : Page
    {
        private const int UsersPageSize = 8;
        private readonly ProfileService.DashboardStatisticsService dashboardStatisticsService = new ProfileService.DashboardStatisticsService();
        private readonly UserAccountService userAccountService = new UserAccountService();
        private readonly AuditService auditService = new AuditService();

        public DashboardStatisticsDto Statistics { get; private set; }

        public string ChartLabelsJson { get; private set; }

        public string ChartValuesJson { get; private set; }

        protected int UsersCurrentPage
        {
            get { return ViewState["UsersCurrentPage"] == null ? 1 : (int)ViewState["UsersCurrentPage"]; }
            set { ViewState["UsersCurrentPage"] = value; }
        }

        protected int UsersTotalPages { get; private set; }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EnsureAdminAccess())
            {
                return;
            }

            Guid adminId;
            if (Guid.TryParse(Context.User.Identity.Name, out adminId)) auditService.RecordAdminDashboardAccess(adminId);

            Statistics = dashboardStatisticsService.GetStatistics();
            ChartLabelsJson = JsonConvert.SerializeObject(Statistics.RegistrationChartLabels);
            ChartValuesJson = JsonConvert.SerializeObject(Statistics.RegistrationChartValues);
            registrationChartData.Attributes["data-labels"] = ChartLabelsJson;
            registrationChartData.Attributes["data-values"] = ChartValuesJson;
            BindUsers();
        }

        public string RegistrationTrendText
        {
            get
            {
                var trend = Statistics == null ? 0 : Statistics.RegistrationTrendPercentage;
                return string.Format("{0}{1:0.0}%", trend >= 0 ? "+" : string.Empty, trend);
            }
        }

        protected string GetStateCss(bool isActive) { return isActive ? "admin-state-active" : "admin-state-inactive"; }

        protected string GetStateText(bool isActive) { return isActive ? "Active" : "Inactive"; }

        protected string GetPaginationCss(PaginationItem item)
        {
            return item.IsCurrent ? " active" : item.IsEllipsis ? " disabled" : string.Empty;
        }

        private void BindUsers()
        {
            using (var context = WebsiteDbContextFactory.Create())
            {
                var search = SearchTextBox.Text.Trim();
                var status = EmploymentStatusDropDownList.SelectedValue;
                var accountState = AccountStateDropDownList.SelectedValue;
                var query = context.UserAccounts.AsQueryable();
                if (!string.IsNullOrWhiteSpace(search)) query = query.Where(user => user.Username.Contains(search) || user.Email.Contains(search));
                EmploymentStatus employmentStatus;
                if (Enum.TryParse(status, out employmentStatus) && Enum.IsDefined(typeof(EmploymentStatus), employmentStatus))
                    query = query.Where(user => user.EmploymentStatus == employmentStatus);
                if (accountState == "Active") query = query.Where(user => user.IsActive);
                if (accountState == "Inactive") query = query.Where(user => !user.IsActive);

                var totalUsers = query.Count();
                UsersTotalPages = Math.Max(1, (int)Math.Ceiling(totalUsers / (double)UsersPageSize));
                if (UsersCurrentPage > UsersTotalPages) UsersCurrentPage = UsersTotalPages;
                UsersPreviousButton.Enabled = UsersCurrentPage > 1;
                UsersNextButton.Enabled = UsersCurrentPage < UsersTotalPages;
                UsersRepeater.DataSource = query.OrderByDescending(user => user.RegisteredUtc)
                    .Skip((UsersCurrentPage - 1) * UsersPageSize)
                    .Take(UsersPageSize)
                    .ToList();
                UsersRepeater.DataBind();
                UsersPaginationRepeater.DataSource = BuildPagination(UsersTotalPages, UsersCurrentPage);
                UsersPaginationRepeater.DataBind();
            }
        }

        protected void UsersRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ToggleActive")
            {
                Guid toggleUserId;
                if (!Guid.TryParse(Convert.ToString(e.CommandArgument), out toggleUserId))
                {
                    FilterFeedbackLiteral.Text = "<p class=\"small text-danger mt-2 mb-0\">The selected user could not be identified.</p>";
                    return;
                }

                var statusChanged = userAccountService.ToggleActive(toggleUserId);
                if (statusChanged)
                {
                    Guid adminId;
                    if (Guid.TryParse(Context.User.Identity.Name, out adminId)) auditService.RecordAdminUserStatusChanged(adminId);
                }
                FilterFeedbackLiteral.Text = statusChanged
                    ? "<p class=\"small text-success mt-2 mb-0\">The user's account state was updated.</p>"
                    : "<p class=\"small text-danger mt-2 mb-0\">The selected user could not be found.</p>";
                Statistics = dashboardStatisticsService.GetStatistics();
                ChartLabelsJson = JsonConvert.SerializeObject(Statistics.RegistrationChartLabels);
                ChartValuesJson = JsonConvert.SerializeObject(Statistics.RegistrationChartValues);
                registrationChartData.Attributes["data-labels"] = ChartLabelsJson;
                registrationChartData.Attributes["data-values"] = ChartValuesJson;
                BindUsers();
                return;
            }

            if (e.CommandName != "DeleteUser")
            {
                return;
            }

            Guid userId;
            if (!Guid.TryParse(Convert.ToString(e.CommandArgument), out userId))
            {
                FilterFeedbackLiteral.Text = "<p class=\"small text-danger mt-2 mb-0\">The selected user could not be identified.</p>";
                return;
            }

            if (userAccountService.Delete(userId))
            {
                Guid adminId;
                if (Guid.TryParse(Context.User.Identity.Name, out adminId)) auditService.RecordAdminUserDeleted(adminId);
                FilterFeedbackLiteral.Text = "<p class=\"small text-success mt-2 mb-0\">The user was deleted successfully.</p>";
                Statistics = dashboardStatisticsService.GetStatistics();
                ChartLabelsJson = JsonConvert.SerializeObject(Statistics.RegistrationChartLabels);
                ChartValuesJson = JsonConvert.SerializeObject(Statistics.RegistrationChartValues);
                registrationChartData.Attributes["data-labels"] = ChartLabelsJson;
                registrationChartData.Attributes["data-values"] = ChartValuesJson;
            }
            else
            {
                FilterFeedbackLiteral.Text = "<p class=\"small text-danger mt-2 mb-0\">The selected user could not be found.</p>";
            }

            BindUsers();
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            UsersCurrentPage = 1;
            string searchTerm = Server.HtmlEncode(SearchTextBox.Text.Trim());
            SearchFeedbackLiteral.Text = string.IsNullOrWhiteSpace(searchTerm)
                ? "<p class=\"small text-secondary mt-2 mb-0\">Enter a username or email to search.</p>"
                : "<p class=\"small text-secondary mt-2 mb-0\">Search submitted for the placeholder view. Data queries will be connected later.</p>";
            BindUsers();
        }

        protected void ApplyFiltersButton_Click(object sender, EventArgs e)
        {
            UsersCurrentPage = 1;
            BindUsers();
        }

        protected void ClearFiltersButton_Click(object sender, EventArgs e)
        {
            SearchTextBox.Text = string.Empty;
            EmploymentStatusDropDownList.SelectedValue = "All";
            AccountStateDropDownList.SelectedValue = "All";
            UsersCurrentPage = 1;
            BindUsers();
        }

        protected void UsersPaginationRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "Page") return;
            int page;
            if (!int.TryParse(Convert.ToString(e.CommandArgument), out page)) return;
            ResetUserFilters();
            UsersCurrentPage = Math.Max(1, page);
            BindUsers();
        }

        protected void UsersPreviousButton_Click(object sender, EventArgs e)
        {
            ResetUserFilters();
            UsersCurrentPage = Math.Max(1, UsersCurrentPage - 1);
            BindUsers();
        }

        protected void UsersNextButton_Click(object sender, EventArgs e)
        {
            ResetUserFilters();
            UsersCurrentPage++;
            BindUsers();
        }

        private void ResetUserFilters()
        {
            SearchTextBox.Text = string.Empty;
            EmploymentStatusDropDownList.SelectedValue = "All";
            AccountStateDropDownList.SelectedValue = "All";
        }

        //protected void FilterButton_Click(object sender, EventArgs e)
        //{
        //    string selectedStatus = Server.HtmlEncode(StatusFilterDropDownList.SelectedItem.Text);
        //    FilterFeedbackLiteral.Text = string.Format(
        //        "<p class=\"small text-secondary mt-2 mb-0\">Filter submitted for {0}. Data queries will be connected later.</p>",
        //        selectedStatus);
        //}

        private bool EnsureAdminAccess()
        {
            if (Context.User == null || !Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect(ResolveUrl("~/login"), false);
                Context.ApplicationInstance.CompleteRequest();
                return false;
            }

            if (!Context.User.IsInRole("Admin"))
            {
                Response.Redirect(ResolveUrl("~/home"), false);
                Context.ApplicationInstance.CompleteRequest();
                return false;
            }

            return true;
        }
    }
}
