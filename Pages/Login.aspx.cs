using System;
using System.Web.UI;
using System.Web.Security;
using VelascoPersonalWebsite_IPT.Application.DTOs;
using VelascoPersonalWebsite_IPT.Application.Services;
using VelascoPersonalWebsite_IPT.DataAccess.Enums;

namespace VelascoPersonalWebsite_IPT.Pages
{
    public partial class Login : Page
    {
        private readonly AuthenticationService authenticationService = new AuthenticationService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.Equals(Request.QueryString["logout"], "1", StringComparison.Ordinal))
            {
                SignOutCurrentUser();
                Response.Redirect(ResolveUrl("~/login"), false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            LoginErrorLabel.Visible = false;

            if (!Page.IsValid)
            {
                return;
            }

            var issuedUtc = DateTime.UtcNow;
            var expiresUtc = issuedUtc.AddHours(1);
            var session = authenticationService.SignIn(
                UsernameTextBox.Text,
                PasswordTextBox.Text,
                issuedUtc,
                expiresUtc);

            if (session == null)
            {
                LoginErrorLabel.Text = "The username or password is invalid.";
                LoginErrorLabel.Visible = true;
                return;
            }

            IssueAuthenticationCookie(session, RememberMeCheckBox.Checked);
            var redirectUrl = session.Role == Role.Admin
                ? ResolveUrl("~/admin")
                : ResolveUrl("~/home");
            Response.Redirect(redirectUrl, false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private static void IssueAuthenticationCookie(AuthenticatedSessionDto session, bool isPersistent)
        {
            var ticket = new FormsAuthenticationTicket(
                2,
                session.UserId.ToString("D"),
                session.IssuedUtc.ToLocalTime(),
                session.ExpiresUtc.ToLocalTime(),
                isPersistent,
                session.Role.ToString(),
                FormsAuthentication.FormsCookiePath);

            var cookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket))
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Path = FormsAuthentication.FormsCookiePath
            };

            if (isPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }

            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private void SignOutCurrentUser()
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie != null && !string.IsNullOrWhiteSpace(cookie.Value))
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                Guid userId;
                if (ticket != null && Guid.TryParse(ticket.Name, out userId))
                {
                    authenticationService.MarkOffline(userId);
                }
            }

            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
        }
    }
}
