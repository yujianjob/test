﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LazyAtHome.API.Mobile
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }

            routes.MapRoute(
              name: "Default",
              url: "{action}",
              defaults: new { controller = "Home", action = "{action}", id = UrlParameter.Optional }
            );

            routes.MapRoute(
             name: "Default1",
             url: "{controller}/{action}",
             defaults: new { controller = "{controller}", action = "{action}", id = UrlParameter.Optional }
           );
        }
    }
}
