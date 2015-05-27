using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WashSite.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult IndexLeftNav()
        {
            var rtn = WCF.Wash.Contract.ClientProxy.web_ProductClient.Cache_web_Wash_Class_SELECT_List();
            return PartialView("_IndexLeftNav", rtn.OBJ);
        }

        public PartialViewResult IndexDayTop()
        {
            var rtn = WCF.Wash.Contract.ClientProxy.web_ProductClient.Cache_web_Wash_Product_SELECT_Recommend(AppConfig.SiteID);
            return PartialView("_IndexDayTop", rtn.OBJ);
        }

        public PartialViewResult Header()
        {
            return PartialView("_Header");
        }

        public PartialViewResult Nav(string index)
        {
            if (index != null)
                ViewBag.Index = index;
            return PartialView("_Nav");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Media()
        {
            return View();
        }

        public ActionResult MediaShow()
        {
            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public ActionResult LaundryTips()
        {
            return View();
        }

        public ActionResult TipsShow(object id)
        {
            return View("TipsShow", id);
        }

        public ActionResult Job()
        {
            return View();
        }

        public ActionResult Agreement()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        public ActionResult tests()
        {
            return View();
        }

        public ActionResult Err(string msg = "")
        {

            return View("Error", "", msg);
        }
    }
}