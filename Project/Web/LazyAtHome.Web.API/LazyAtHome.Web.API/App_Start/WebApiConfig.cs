using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace LazyAtHome.Web.API
{
    public static class WebApiConfig
    {
        /// <summary>
        /// 加密串 
        /// </summary>
        public const string Key = "lz123456789012345678901234567890";

        //public const string ExpAppDownloadUrl = "http://newsyue.8866.org:85/base/Exp_android_download";
        //public const string ExpAppDownloadUrl = "http://api.landaojia.com/webapi/base/Exp_android_download";

        //public static int ExpAppVersionCode = Convert.ToInt32();
        //public static string ExpAppDownloadUrl = ConfigurationManager.AppSettings["ExpressMobile:DownloadUrl"];

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        public static bool ExpressMobile_Test = ConfigurationManager.AppSettings["ExpressMobile:Test"] == "1";
    }
}
