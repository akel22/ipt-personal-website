using System;
using System.Linq;
using VelascoPersonalWebsite_IPT.Application.DTOs;
using VelascoPersonalWebsite_IPT.Application.Services;

namespace VelascoPersonalWebsite_IPT
{
    public partial class Home : System.Web.UI.Page
    {
        private readonly ProfileService profileService = new ProfileService();

        public ProfileDto PublicProfile { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User == null || !Context.User.Identity.IsAuthenticated)
            {
                Response.Redirect(ResolveUrl("~/login"), false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            PublicProfile = profileService.GetOrCreate();
            FooterStatementRepeater.DataSource = PublicProfile.FooterStatement.Split('|').Select(line => line.Trim()).Where(line => line.Length > 0);
            FooterStatementRepeater.DataBind();
        }
    }
}
