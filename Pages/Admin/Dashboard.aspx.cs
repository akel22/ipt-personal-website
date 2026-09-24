using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Linq;
using VelascoPersonalWebsite_IPT.Application.DTOs;
using VelascoPersonalWebsite_IPT.Application.Services;
using VelascoPersonalWebsite_IPT.DataAccess.DbContext;

namespace VelascoPersonalWebsite_IPT.Pages.Admin
{
    public partial class Dashboard : Page
    {
        private readonly ProfileService.DashboardStatisticsService dashboardStatisticsService = new ProfileService.DashboardStatisticsService();
        private readonly UserAccountService userAccountService = new UserAccountService();

        public DashboardStatisticsDto Statistics { get; private set; }

        public string ChartLabelsJson { get; private set; }

        public string ChartValuesJson { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EnsureAdminAccess())
            {
                return;
            }

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

        private void BindUsers()
        {
            using (var context = WebsiteDbContextFactory.Create())
            {
                var search = SearchTextBox.Text.Trim();
                var status = StatusFilterDropDownList.SelectedValue;
                var query = context.UserAccounts.AsQueryable();
                if (!string.IsNullOrWhiteSpace(search)) query = query.Where(user => user.Username.Contains(search) || user.Email.Contains(search));
                if (status != "All") query = query.Where(user => user.EmploymentStatus.ToString() == status);
                UsersRepeater.DataSource = query.OrderByDescending(user => user.RegisteredUtc).ToList();
                UsersRepeater.DataBind();
            }
        }

        protected void UsersRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
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
            string searchTerm = Server.HtmlEncode(SearchTextBox.Text.Trim());
            SearchFeedbackLiteral.Text = string.IsNullOrWhiteSpace(searchTerm)
                ? "<p class=\"small text-secondary mt-2 mb-0\">Enter a username or email to search.</p>"
                : "<p class=\"small text-secondary mt-2 mb-0\">Search submitted for the placeholder view. Data queries will be connected later.</p>";
        }

        protected void FilterButton_Click(object sender, EventArgs e)
        {
            string selectedStatus = Server.HtmlEncode(StatusFilterDropDownList.SelectedItem.Text);
            FilterFeedbackLiteral.Text = string.Format(
                "<p class=\"small text-secondary mt-2 mb-0\">Filter submitted for {0}. Data queries will be connected later.</p>",
                selectedStatus);
        }

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
