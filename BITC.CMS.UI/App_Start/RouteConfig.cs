using BITC.Web.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BITC.CMS.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{culture}/Account/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional, culture = "vi-VN" }
            );

            routes.MapRoute(
                name: "Page",
                url: "{culture}/{*url}",
                defaults: new { controller = "Page", action = "Index", url = UrlParameter.Optional, culture = "vi-VN" },
                namespaces: new[] { "BITC.CMS.UI.Controllers" }
            );
        }
    }
}
