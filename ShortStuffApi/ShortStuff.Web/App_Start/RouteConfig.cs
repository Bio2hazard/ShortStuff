// ShortStuff.Web
// RouteConfig.cs
// 
// Licensed under GNU GPL v2.0
// See License/GPLv2.txt for details

using System.Web.Mvc;
using System.Web.Routing;

namespace ShortStuff.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "Default", url: "{controller}/{action}/{id}", defaults: new
            {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            });
        }
    }
}
