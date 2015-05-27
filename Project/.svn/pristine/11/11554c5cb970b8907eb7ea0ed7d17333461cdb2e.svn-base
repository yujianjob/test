using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WeiXin3.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            string location = HttpContext.Cache[AppConfig.Cache_Location + AppConfig.WeiXin_UserOpenID].ToString();

            string requestUrl = "http://api.map.baidu.com/geocoder/v2/?ak=" + AppConfig.Baidu_AK + "&output=xml&location=" + location + "&coordtype=wgs84ll";
            string str = App_Code.UtilityFunc.WebGetRequest(requestUrl);

            ViewBag.Message = str;

            return View();
        }
    }
}