using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmailClient.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            MapMailRoutes(routes);

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }

        private static void MapMailRoutes(RouteCollection routes)
        {          

            routes.MapRoute(
                name:"mailRoot",
                url:"mail/index",
                defaults: new { controller = "Mail", action = "Index"}
                );

            routes.MapRoute(
                name: "mailCatchAll",
                url: "mail/{*catchall}",
                defaults: new { controller = "Mail", action = "Index" }

                );
        }
    }
}
