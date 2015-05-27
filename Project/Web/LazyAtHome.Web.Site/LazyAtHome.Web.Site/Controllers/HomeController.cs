using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.Site.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public string About()
        {
            //Core.Helper.CacheHelper.Put("testKey11", "杨尤丰pig pig");

            var rtn = Core.Helper.CacheHelper.Get("testKey11");

            return rtn.ToString();
        }

        public string Contact()
        {
            var rtn = Core.Helper.CacheHelper.Get("testKey11");

            return rtn.ToString();
        }
    }
}