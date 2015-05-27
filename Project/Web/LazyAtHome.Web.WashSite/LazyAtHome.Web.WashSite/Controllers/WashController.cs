using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.WCF.Wash.Contract.DataContract.web;

namespace LazyAtHome.Web.WashSite.Controllers
{
    public class WashController : BaseController
    {
        //
        // GET: /Wash/
        public ActionResult Index()
        {
            var rtn = WCF.Wash.Contract.ClientProxy.web_ProductClient.Cache_web_Wash_Class_SELECT_List_Second_Detail(Site);
            if (rtn.Success)
            {
                
            }
            return View(rtn.OBJ);
        }

        public ActionResult Search(string city, string keyword)
        {
            ViewBag.KeyWord = keyword;
            var rtn = App_Code.Proxy.WashProxy.Select_Category_Search(keyword, AppConfig.SiteID, 1, 20);
            return View("index", rtn.OBJ.ReturnList);
        }

        public ActionResult Show(int id = 1, int? subid = null)
        {
            var user = GetUserInfo();
            ViewBag.Cart = user.Cart;
            var rtn = web_ProductClient.Cache_web_Wash_Category_SELECT_Entity(id);
            if (rtn.Success && rtn.OBJ.ProductList.Count == 1)
                subid = rtn.OBJ.ProductList[0].ID;

            ViewBag.SID = subid;
            return View(rtn.OBJ);
        }

        public ActionResult Activities()
        {
            return View();
        }

        public PartialViewResult MoreWash()
        {
            var rtn = WCF.Wash.Contract.ClientProxy.web_ProductClient.Cache_web_Wash_Product_SELECT_Recommend(AppConfig.SiteID);
            return PartialView("MoreWashPartial", rtn.OBJ);            
        }
	}
}