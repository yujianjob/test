using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.API.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult Exp_android_download()
        {
            return File("E:/File/Mobile/landaojia/Android/kuaidi.apk", "application/octet-stream", "kuaidi.apk");
        }
    }
}