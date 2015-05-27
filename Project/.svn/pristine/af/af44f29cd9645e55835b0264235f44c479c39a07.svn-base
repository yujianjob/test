using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ApplicationServer.Caching;

namespace LazyAtHome.Core.Infrastructure.Cache
{
    public class AppFabricService
    {
        public DataCache GetCache(Enumerate.CacheModule module = Enumerate.CacheModule.Default)
        {
            string cacheName = string.Empty;

            //if (System.Web.HttpContext.Current != null)
            //{
            //    //string configpath = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\web.config";
            //    //var config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
            //    //cacheName = config.AppSettings.Settings["CacheName"].Value;
            //}
            //else
            //{
            //    //string configpath = System.Reflection.Assembly.GetEntryAssembly().Location + ".config";
            //    //XmlDocument xmlDoc = new XmlDocument();
            //    //xmlDoc.Load(configpath);

            //    //XmlNode node = xmlDoc.DocumentElement.SelectNodes("/configuration/appSettings/CacheConfig")[0];
            //    //if (node != null)
            //    //    cacheName = node.Attributes["CacheName"].InnerText;
            //    cacheName = System.Configuration.ConfigurationManager.AppSettings["CacheName"];

            //}
            cacheName = System.Configuration.ConfigurationManager.AppSettings["CacheName"];
            //if (string.IsNullOrWhiteSpace(cacheName))
            //    cacheName = "";

            //switch (module)
            //{
            //    case Enumerate.CacheModule.WebManage:
            //        cacheName = "default";
            //        break;
            //    case Enumerate.CacheModule.Web:
            //        cacheName = "default";
            //        break;
            //    default:
            //        cacheName = "default";
            //        break;
            //}

            DataCacheFactory mycacheFactory = new DataCacheFactory();
            DataCache myCache = mycacheFactory.GetCache(cacheName);
            return myCache;
        }
    }
}
