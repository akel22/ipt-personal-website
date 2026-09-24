using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Security.Principal;
using System.Security.Cryptography;
using VelascoPersonalWebsite_IPT.Application.Services;

namespace IPT_VelascoPersonalWebsite
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null || string.IsNullOrWhiteSpace(cookie.Value))
            {
                return;
            }

            FormsAuthenticationTicket ticket;
            try
            {
                ticket = FormsAuthentication.Decrypt(cookie.Value);
            }
            catch (HttpException)
            {
                FormsAuthentication.SignOut();
                return;
            }
            catch (ArgumentException)
            {
                FormsAuthentication.SignOut();
                return;
            }
            catch (CryptographicException)
            {
                FormsAuthentication.SignOut();
                return;
            }

            if (ticket == null || ticket.Expired)
            {
                MarkTicketUserOffline(ticket);
                FormsAuthentication.SignOut();
                return;
            }

            Guid userId;
            if (!Guid.TryParse(ticket.Name, out userId))
            {
                FormsAuthentication.SignOut();
                return;
            }

            var authenticationService = new AuthenticationService();
            if (!authenticationService.IsActiveUser(userId))
            {
                authenticationService.MarkOffline(userId);
                FormsAuthentication.SignOut();
                return;
            }

            Context.User = new GenericPrincipal(
                new GenericIdentity(ticket.Name, "Forms"),
                new[] { ticket.UserData });
        }

        void Application_AuthorizeRequest(object sender, EventArgs e)
        {
            var path = Request.Url.AbsolutePath.TrimEnd('/');
            var isHome = string.Equals(path, ToAbsolutePath("~/home"), StringComparison.OrdinalIgnoreCase);
            var adminPath = ToAbsolutePath("~/admin");
            var isAdmin = string.Equals(path, adminPath, StringComparison.OrdinalIgnoreCase) ||
                path.StartsWith(adminPath + "/", StringComparison.OrdinalIgnoreCase);

            if (!isHome && !isAdmin)
            {
                return;
            }

            if (Context.User == null || !Context.User.Identity.IsAuthenticated)
            {
                RedirectToLogin();
                return;
            }

            if (isAdmin && !Context.User.IsInRole("Admin"))
            {
                Response.Redirect(ToAbsolutePath("~/home"), false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private static void MarkTicketUserOffline(FormsAuthenticationTicket ticket)
        {
            Guid userId;
            if (ticket != null && Guid.TryParse(ticket.Name, out userId))
            {
                new AuthenticationService().MarkOffline(userId);
            }
        }

        private void RedirectToLogin()
        {
            FormsAuthentication.SignOut();
            Response.Redirect(ToAbsolutePath("~/login"), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        private string ToAbsolutePath(string virtualPath)
        {
            return VirtualPathUtility.ToAbsolute(virtualPath, Request.ApplicationPath).TrimEnd('/');
        }
    }
}