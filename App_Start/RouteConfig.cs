using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace IPT_VelascoPersonalWebsite
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Home", "home", "~/Home.aspx");
            routes.MapPageRoute("Login", "login", "~/Pages/Login.aspx");
            routes.MapPageRoute("Root", "", "~/Pages/Login.aspx");
            routes.MapPageRoute("Register", "register", "~/Pages/Register.aspx");

            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);
        }
    }
}
