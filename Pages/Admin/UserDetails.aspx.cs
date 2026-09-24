using System;
using System.Web.UI;
using VelascoPersonalWebsite_IPT.DataAccess.DbContext;

namespace VelascoPersonalWebsite_IPT.Pages.Admin
{
    public partial class UserDetails : Page
    {
        public string UsernameValue { get; private set; }
        public string EmailValue { get; private set; }
        public string EmploymentStatusValue { get; private set; }
        public string GenderValue { get; private set; }
        public string DateOfBirthValue { get; private set; }
        public string AccountStateValue { get; private set; }
        public string RegisteredValue { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Guid userId;
            if (!Guid.TryParse(Request.QueryString["id"], out userId)) { ShowNotFound(); return; }
            using (var context = WebsiteDbContextFactory.Create())
            {
                var user = context.UserAccounts.Find(userId);
                if (user == null) { ShowNotFound(); return; }
                UsernameValue = user.Username; EmailValue = user.Email; EmploymentStatusValue = user.EmploymentStatus.ToString();
                GenderValue = user.Gender.ToString(); DateOfBirthValue = user.DateOfBirth.ToString("yyyy-MM-dd");
                AccountStateValue = user.IsActive ? "Active" : "Inactive"; RegisteredValue = user.RegisteredUtc.ToString("yyyy-MM-dd HH:mm");
                DetailsPanel.Visible = true;
            }
        }

        private void ShowNotFound()
        {
            FeedbackLiteral.Text = "<div class=\"alert alert-warning\">The requested user could not be found.</div>";
        }
    }
}
